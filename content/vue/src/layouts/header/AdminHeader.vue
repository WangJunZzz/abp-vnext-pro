<template>
  <div>
    <a-layout-header :class="[headerTheme, 'admin-header']">
      <div :class="['admin-header-wide', layout, pageWidth]">
        <router-link
          v-if="isMobile || layout === 'head'"
          to="/"
          :class="['logo', isMobile ? null : 'pc', headerTheme]"
        >
          <img width="32" src="@/assets/img/logo.png" />
          <h1 v-if="!isMobile">{{ systemName }}</h1>
        </router-link>
        <a-divider v-if="isMobile" type="vertical" />
        <a-icon
          v-if="layout !== 'head'"
          class="trigger"
          :type="collapsed ? 'menu-unfold' : 'menu-fold'"
          @click="toggleCollapse"
        />
        <div
          v-if="layout !== 'side' && !isMobile"
          class="admin-header-menu"
          :style="`width: ${menuWidth};`"
        >
          <i-menu
            class="head-menu"
            :theme="headerTheme"
            mode="horizontal"
            :options="menuData"
            @select="onSelect"
          />
        </div>
        <div :class="['admin-header-right', headerTheme]">
          <header-search
            class="header-item"
            @active="(val) => (searchActive = val)"
          />
          <!-- <a-tooltip class="header-item" title="帮助文档" placement="bottom" >
            <a href="https://iczer.gitee.io/vue-antd-admin-docs/" target="_blank">
              <a-icon type="question-circle-o" />
            </a>
          </a-tooltip> -->

          <header-avatar class="header-item" />
          <a-dropdown class="lang header-item">
            <div><a-icon type="global" /> {{ langAlias }}</div>
            <a-menu
              @click="switchLanguage"
              :selected-keys="[lang]"
              slot="overlay"
            >
              <a-menu-item v-for="lang in langList" :key="lang.key">{{
                lang.key.toLowerCase() + " " + lang.name
              }}</a-menu-item>
            </a-menu>
          </a-dropdown>
        </div>
      </div>
    </a-layout-header>
  </div>
</template>

<script>
import HeaderSearch from "./HeaderSearch";

import HeaderAvatar from "./HeaderAvatar";
import IMenu from "@/components/menu/menu";
import { mapState, mapMutations } from "vuex";
import { getAbpApplicationConfiguration } from "@/services/admin/user";
export default {
  name: "AdminHeader",
  components: { IMenu, HeaderAvatar, HeaderSearch },
  props: ["collapsed", "menuData"],
  data() {
    return {
      langList: [
        { key: "CN", name: "简体中文", alias: "简体" },
        { key: "US", name: "English", alias: "English" },
      ],
      searchActive: false,
    };
  },
  computed: {
    ...mapState("setting", [
      "theme",
      "isMobile",
      "layout",
      "systemName",
      "lang",
      "pageWidth",
    ]),
    headerTheme() {
      if (
        this.layout == "side" &&
        this.theme.mode == "dark" &&
        !this.isMobile
      ) {
        return "light";
      }
      return this.theme.mode;
    },
    langAlias() {
      let lang = this.langList.find((item) => item.key == this.lang);
      return lang.alias;
    },
    menuWidth() {
      const { layout, searchActive } = this;
      const headWidth = layout === "head" ? "100% - 188px" : "100%";
      const extraWidth = searchActive ? "600px" : "400px";
      return `calc(${headWidth} - ${extraWidth})`;
    },
  },
  methods: {
    ...mapMutations("setting", ["setLang"]),
    toggleCollapse() {
      this.$emit("toggleCollapse");
    },
    onSelect(obj) {
      this.$emit("menuSelect", obj);
    },
    switchLanguage(val) {
      this.setLang(val.key);
      this.$store.commit("setDefalutLanguage", this.languagesMapping(val.key));
      this.getAbpConfiguration();
    },
    languagesMapping(language) {
      if (language == "US") {
        return "en";
      } else {
        return "zh-Hans";
      }
    },
    getAbpConfiguration() {
      getAbpApplicationConfiguration().then((res) => {
        res = res.data;
        // 获取多语言集合
        this.$store.commit("setLanguages", res.localization.languages);
        // 如果后台项目的名称修改了 这个Zzz要替换
        // 获取多语言资源
        this.$store.commit("setLocalization", res.localization.values.Zzz);
        // 获取用户权限
        this.$store.commit("setPermissions", res.auth.grantedPolicies);

        //this.$router.push("/dashboard");
      });
    },
  },
};
</script>

<style lang="less" scoped>
@import "index";
</style>

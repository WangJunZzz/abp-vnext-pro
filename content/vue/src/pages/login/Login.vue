<template>
  <common-layout>
    <div class="top">
      <div class="header">
        <img alt="logo" class="logo" src="@/assets/img/logo.png" />
        <span class="title">{{ systemName }}</span>
      </div>
    </div>
    <div class="login">
      <a-form @submit="onSubmit" :form="form" style="margin-top: 100px">
        <a-alert
          type="error"
          :closable="true"
          v-show="error"
          :message="error"
          showIcon
          style="margin-bottom: 24px"
        />
        <a-form-item>
          <a-input
            autocomplete="autocomplete"
            size="large"
            placeholder=""
            v-decorator="[
              'name',
              {
                rules: [
                  {
                    required: true,
                    message: '请输入账户名',
                    whitespace: true,
                  },
                ],
              },
            ]"
          >
            <a-icon slot="prefix" type="user" />
          </a-input>
        </a-form-item>
        <a-form-item>
          <a-input
            size="large"
            placeholder=""
            autocomplete="autocomplete"
            type="password"
            v-decorator="[
              'password',
              {
                rules: [
                  {
                    required: true,
                    message: '请输入密码',
                    whitespace: true,
                  },
                ],
              },
            ]"
          >
            <a-icon slot="prefix" type="lock" />
          </a-input>
        </a-form-item>
        <div>
          <!-- <a-checkbox :checked="true">自动登录</a-checkbox>
          <a style="float: right">忘记密码</a> -->
        </div>
        <a-form-item>
          <a-button
            :loading="logging"
            style="width: 100%; margin-top: 24px"
            size="large"
            htmlType="submit"
            type="primary"
            >登录</a-button
          >
        </a-form-item>
        <div>
          其他登录方式
          <a-icon class="icon" type="alipay-circle" />
          <a-icon class="icon" type="taobao-circle" />
          <a-icon class="icon" type="weibo-circle" />
          <router-link style="float: right" to="/dashboard/workplace"
            >注册账户</router-link
          >
        </div>
      </a-form>
    </div>
  </common-layout>
</template>

<script>
import CommonLayout from "@/layouts/CommonLayout";
import { login, getAbpApplicationConfiguration } from "@/services/admin/user";

export default {
  i18n: require("./i18n"),
  name: "Login",
  components: { CommonLayout },
  data() {
    return {
      logging: false,
      error: "",
      form: this.$form.createForm(this),
    };
  },
  computed: {
    systemName() {
      return this.$store.state.setting.systemName;
    },
  },
  methods: {
    onSubmit(e) {
      e.preventDefault();
      this.form.validateFields((err) => {
        if (!err) {
          this.logging = true;
          const name = this.form.getFieldValue("name");
          const password = this.form.getFieldValue("password");
          login(name, password).then(this.afterLogin);
        }
      });
    },
    afterLogin(res) {
      res = res.data;
      if (res.code == 200) {
        let userInfo = {
          userName: res.data.name,
          name: res.data.name,
          token: res.data.token,
        };
        this.$store.commit("setUserInfo", userInfo);
        this.getAbpConfiguration();
      } else {
        this.logging = false;

        this.$message.error(this.$t("loginResult"));
      }
    },
    getAbpConfiguration() {
      getAbpApplicationConfiguration().then((res) => {
        this.logging = false;
        res = res.data;
        // 获取多语言集合
        this.$store.commit("setLanguages", res.localization.languages);
        // 如果后台项目的名称修改了 这个Zzz要替换
        // 获取多语言资源
        this.$store.commit("setLocalization", res.localization.values.Zzz);
        // 获取用户权限
        this.$store.commit("setPermissions", res.auth.grantedPolicies);

        this.$router.push("/dashboard");
      });
    },
  },
};
</script>

<style lang="less" scoped>
.common-layout {
  .top {
    text-align: center;
    .header {
      height: 44px;
      line-height: 44px;
      a {
        text-decoration: none;
      }
      .logo {
        height: 44px;
        vertical-align: top;
        margin-right: 16px;
      }
      .title {
        font-size: 33px;
        color: @title-color;
        font-family: "Myriad Pro", "Helvetica Neue", Arial, Helvetica,
          sans-serif;
        font-weight: 600;
        position: relative;
        top: 2px;
      }
    }
    .desc {
      font-size: 14px;
      color: @text-color-second;
      margin-top: 12px;
      margin-bottom: 40px;
    }
  }
  .login {
    width: 368px;
    margin: 0 auto;
    @media screen and (max-width: 576px) {
      width: 95%;
    }
    @media screen and (max-width: 320px) {
      .captcha-button {
        font-size: 14px;
      }
    }
    .icon {
      font-size: 24px;
      color: @text-color-second;
      margin-left: 16px;
      vertical-align: middle;
      cursor: pointer;
      transition: color 0.3s;

      &:hover {
        color: @primary-color;
      }
    }
  }
}
</style>

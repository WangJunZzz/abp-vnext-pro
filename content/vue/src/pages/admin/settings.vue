<template>
  <div>
    <a-card :bordered="false">
      <a-spin v-if="spinning" />
      <a-tabs default-active-key="1" tab-position="left">
        <a-tab-pane
          v-for="item in tabList"
          :key="item.groupName"
          :tab="item.groupDisplayName"
        >
          <a-form-model :label-col="{ span: 5 }" :wrapper-col="{ span: 12 }">
            <a-form-model-item
              v-for="info in item.settingInfos"
              :key="info.name"
              :label="info.displayName"
            >
              <div
                v-if="
                  info.properties.Type === 'number' ||
                    info.properties.Type === 'text'
                "
              >
                <a-input
                  v-model="info.value"
                  :id="info.name"
                  :name="info.formName"
                  size="small"
                />
              </div>

              <div v-if="info.properties.Type === 'checkbox'">
                <a-checkbox
                  v-model="info.value"
                  :checked="info.value"
                  size="small"
                >
                  {{ info.description }}
                </a-checkbox>
              </div>

              <div v-if="info.properties.Type === 'select'">
                <a-select
                  default-value="info.value"
                  v-model="info.value"
                  style="width: 120px"
                  size="small"
                >
                  <a-select-option
                    v-for="itemOption in info.properties.Options.split('|')"
                    :value="itemOption"
                    :key="itemOption"
                  >
                    {{ itemOption }}
                  </a-select-option>
                </a-select>
              </div>
            </a-form-model-item>
            <a-button
              size="small"
              style="margin-left:65%"
              type="primary"
              @click="updateSettingValues(item.settingInfos)"
              >{{ $t("save") }}</a-button
            >
          </a-form-model>
        </a-tab-pane>
      </a-tabs></a-card
    >
  </div>
</template>

<script>
import { getSettingsAll, putSettings } from "@/services/admin/settings";
export default {
  i18n: require("./i18n"),
  data() {
    return {
      tabList: [],
      spinning: true,
    };
  },
  created() {
    this.init();
  },
  methods: {
    init() {
      getSettingsAll().then((res) => {
        res.data.forEach((e) => {
          let item = {
            groupName: e.groupName,
            groupDisplayName: e.groupDisplayName,
            settingInfos: e.settingInfos,
          };
          e.settingInfos.map((el) => {
            if (el.properties.Type === "checkbox") {
              if (el.value === "True" || el.value === "true") {
                el.value = true;
              } else {
                el.value = false;
              }
            }
          });
          this.tabList.push(item);
          this.spinning = false;
        });
      });
    },

    updateSettingValues(item) {
      const obj = {};
      const prefix = "setting_";
      item.forEach((e) => {
        obj[prefix + e.name] = String(e.value);
      });

      putSettings(obj).then(() => {
        this.$message.success(this.$t("successMsg"));
      });
    },
  },
};
</script>

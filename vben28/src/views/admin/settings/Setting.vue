<template>
  <PageWrapper v-loading="loading">
    <div ref="wrapperRef" :class="prefixCls">
      <Tabs tab-position="left" :tabBarStyle="tabBarStyle">
        <template v-for="item in settingList" :key="item.group">
          <TabPane :tab="item.groupDisplayName">
            <CollapseContainer :title="item.groupDisplayName" :canExpan="false">
              <a-form :label-col="labelCol" :wrapper-col="wrapperCol">
                <a-form-item v-for="setting in item.settingItemOutput" :label="setting.displayName">
                  <div v-if="setting.type === 'Text'">
                    <a-input v-model:value="setting.value" style="width: 80%" />
                  </div>
                  <div v-if="setting.type === 'Number'">
                    <a-input-number v-model:value="setting.value" :min="1" :max="10" />
                  </div>
                  <div v-if="setting.type === 'CheckBox'">
                    <a-checkbox
                      :checked="!(setting.value == 'false' || setting.value == false)"
                      @update:checked="(val) => (setting.value = val)"
                    >
                      {{ setting.description }}
                    </a-checkbox>
                  </div>
                </a-form-item>

                <a-button
                  style="margin-left: 65%"
                  type="primary"
                  @click="updateSettingValues(item.settingItemOutput)"
                  >{{ t('common.saveText') }}</a-button
                >
              </a-form>
            </CollapseContainer>
          </TabPane>
        </template>
      </Tabs>
    </div>
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, reactive, toRefs, onMounted } from 'vue';
  import { Tabs } from 'ant-design-vue';
  import { CollapseContainer,ScrollContainer } from '/@/components/Container/index';
  import { SettingOutput, UpdateSettingInput } from '/@/services/ServiceProxies';
  import { getAllSettingsAsync, updateSettingsAsync } from "/@/views/admin/settings/Setting";
  import { useI18n } from '/@/hooks/web/useI18n';
  import { PageWrapper } from '/@/components/Page';
  import { message } from 'ant-design-vue';
  export default defineComponent({
    components: {
      ScrollContainer,
      CollapseContainer,
      Tabs,
      TabPane: Tabs.TabPane,
      PageWrapper,
    },
    setup() {
      let settingList: SettingOutput[] = [];
      const { t } = useI18n();
      const state = reactive({
        settingList,
        loading: true,
      });
      onMounted(async () => {
        state.loading = true;
        const result = await getAllSettingsAsync();
        state.settingList = result;
        state.loading = false;
      });

      const updateSettingValues = async (item: any) => {
        try {
      
          const prefix = 'setting_';
          const request = new UpdateSettingInput();
          request.values as {};
          let items: { [key: string]: string } = {};
          item.forEach((e) => {
            items[prefix + e.name] = String(e.value);
          });
          request.values = items;
          await updateSettingsAsync({ request });
          message.success(t('common.operationSuccess'));
        } catch (error) {
          message.success(t('common.operationFail'));
        }
      };
      return {
        prefixCls: 'account-setting',
        tabBarStyle: {
          width: '220px',
        },
        labelCol: { span: 4 },
        wrapperCol: { span: 14 },
        ...toRefs(state),
        t,
        updateSettingValues,
      };
    },
  });
</script>
<style lang="less">
  .account-setting {
    margin: 12px;
    background-color: @component-background;

    .base-title {
      padding-left: 0;
    }

    .ant-tabs-tab-active {
      background-color: @item-active-bg;
    }
  }
</style>

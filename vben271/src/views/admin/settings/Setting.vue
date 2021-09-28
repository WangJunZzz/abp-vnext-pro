<template>
  <PageWrapper v-loading="loading">
    <div ref="wrapperRef" :class="prefixCls">
      <Tabs tab-position="left" :tabBarStyle="tabBarStyle">
        <template v-for="item in settingList" :key="item.groupName">
          <TabPane :tab="item.groupDisplayName">
            <CollapseContainer :title="item.groupDisplayName" :canExpan="false">
              <a-form :label-col="labelCol" :wrapper-col="wrapperCol">
                <a-form-item v-for="setting in item.settingInfos" :label="setting.displayName">
                  <div
                    v-if="
                      setting.properties.Type === 'number' || setting.properties.Type === 'text'
                    "
                  >
                    <a-input v-model:value="setting.value" style="width: 80%" />
                  </div>
                  <div v-if="setting.properties.Type === 'checkbox'">
                    <a-checkbox
                      :checked="!(setting.value == 'false' || setting.value == false)"
                      @update:checked="(val) => (setting.value = val)"
                    >
                      {{ setting.description }}
                    </a-checkbox>
                    <!-- <a-checkbox v-model:checked="setting.value" size="small">
                      {{ setting.description }}
                    </a-checkbox> -->
                  </div>

                  <div v-if="setting.properties.Type === 'select'">
                    <a-select default-value="setting.value" style="width: 120px" size="small">
                      <a-select-option
                        v-for="itemOption in setting.properties.Options.split('|')"
                        :value="itemOption"
                        :key="itemOption"
                      >
                        {{ itemOption }}
                      </a-select-option>
                    </a-select>
                  </div>
                </a-form-item>

                <a-button
                  style="margin-left: 65%"
                  type="primary"
                  @click="updateSettingValues(item.settingInfos)"
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
  import { CollapseContainer } from '/@/components/Container/index';
  import { ScrollContainer } from '/@/components/Container/index';
  import { SettingGroup, UpdateSettingInput } from '/@/services/ServiceProxies';
  import { getAllSettingsAsync, updateSettingsAsync } from './Setting';
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
      let settingList: SettingGroup[] = [];
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
          var request = new UpdateSettingInput();
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

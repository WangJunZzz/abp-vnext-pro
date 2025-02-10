<template>
  <BasicModal :title="t('routes.tenant.manageFeatures')" :canFullscreen="false" :defaultFullscreen="true" @ok="submit"
    @cancel="cancel" @register="registerModal">
    <PageWrapper v-loading="loading">
      <div ref="wrapperRef" :class="prefixCls">
        <Tabs tab-position="left" :tabBarStyle="tabBarStyle" @change="change">
          <template v-for="item in featureList" :key="item.name">
            <TabPane :tab="item.displayName">
              <CollapseContainer :title="item.displayName" :canExpan="false">
                <a-form :label-col="labelCol" :wrapper-col="wrapperCol">
                  <a-form-item v-for="feature in item.features" :label="feature.displayName">
                    <div v-if="feature.valueType.name === 'FreeTextStringValueType'">
                      <a-input v-model:value="feature.value" style="width: 80%" />
                    </div>

                    <div v-if="feature.valueType.name === 'ToggleStringValueType'">
                      <a-checkbox :checked="!(feature.value == 'false' || feature.value == false)"
                        @update:checked="(val) => (feature.value = val)">
                      </a-checkbox>
                    </div>
                  </a-form-item>
                </a-form>
              </CollapseContainer>
            </TabPane>
          </template>
        </Tabs>
      </div>
    </PageWrapper>

  </BasicModal>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs } from 'vue';
import { BasicModal, useModalInner } from '/@/components/Modal';
import { useI18n } from '/@/hooks/web/useI18n';
import { Tabs } from 'ant-design-vue';
import { CollapseContainer, ScrollContainer } from '/@/components/Container/index';
import { PageWrapper } from '/@/components/Page';
import { getTenantFeatureListAsync, updateTenantFeatureListAsync } from '/@/views/tenants/Tenant';
import { FeatureGroupDto, UpdateFeatureDto } from '/@/services/ServiceProxies';
import { message } from 'ant-design-vue';
export default defineComponent({
  name: 'ManageFeatrue',
  components: {
    BasicModal,
    ScrollContainer,
    CollapseContainer,
    Tabs,
    TabPane: Tabs.TabPane,
    PageWrapper,
  },
  setup() {
    const { t } = useI18n();
    let featureList: FeatureGroupDto[] = [];

    // 当前租户id
    let tenantId = '';
    // 当前选中的tab页面数据
    let currentFeature: FeatureGroupDto | undefined;
    const state = reactive({
      featureList,
      loading: true,
    });


    const [registerModal] = useModalInner(async (data) => {
      state.loading = true;
      tenantId = data.id;
      const result = await getTenantFeatureListAsync(data.id);
      state.featureList = result.groups as FeatureGroupDto[];
      if (state.featureList.length > 0) {
        currentFeature = state.featureList[0]
      }
      state.loading = false;
    });



    const submit = async () => {

      if (currentFeature) {
        let features = currentFeature.features?.map(e => {
          let item = new UpdateFeatureDto();
          item.name = e.name;
          item.value = e.value?.toString();
          return item;
        })

        try {
          state.loading = true;
          await updateTenantFeatureListAsync(tenantId, features)
          message.success(t('common.operationSuccess'));
        } catch (error) {
          message.success(t('common.operationFail'));
        }finally{
          state.loading = false;
        }

      }
    };
    const change = (key) => {
      currentFeature = state.featureList.find(e => e.name == key);
    };
    const cancel = () => {
    };
    return {
      t,
      registerModal,
      submit,
      cancel,
      ...toRefs(state),
      prefixCls: 'account-setting',
      tabBarStyle: {
        width: '220px',
      },
      labelCol: { span: 4 },
      wrapperCol: { span: 14 },
      change
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

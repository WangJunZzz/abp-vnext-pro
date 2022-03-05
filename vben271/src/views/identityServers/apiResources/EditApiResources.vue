<template>
  <BasicModal
    :title="t('common.editText')"
    :width="700"
    :canFullscreen="false"
    @ok="submit"
    @register="registerModal"
    :bodyStyle="{ 'padding-top': '0' }"
    :destroyOnClose="true"
  >
    <div>
      <Tabs>
        <TabPane tab="Basic" key="1">
          <BasicForm @register="registerBasicInfoForm" />
        </TabPane>
        <TabPane tab="ApiScopes" key="2">
          <a-checkbox-group v-model:value="defaultApiScope">
            <a-row justify="center">
              <a-col :span="24">
                <a-checkbox
                  style="width: 200px"
                  v-for="(item, index) in apiScopes"
                  :key="index"
                  :value="item.value"
                >{{ item.label }}
                </a-checkbox
                >
              </a-col>
            </a-row>
          </a-checkbox-group>
        </TabPane>
      </Tabs>
    </div>
  </BasicModal>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs } from "vue";
import { BasicModal, useModalInner } from "/@/components/Modal";
import { BasicForm, useForm } from "/@/components/Form/index";
import { Tabs } from "ant-design-vue";
import {
  editFormSchema,
  getAllApiScopeAsync,
  updateApiResourceAsync
} from "/@/views/identityServers/apiResources/ApiResources";
import { StringStringFromSelector } from "/@/services/ServiceProxies";
import { useI18n } from "/@/hooks/web/useI18n";

export default defineComponent({
  name: "EditApiResources",
  components: {
    BasicModal,
    BasicForm,
    Tabs,
    TabPane: Tabs.TabPane
  },
  emits: ["reload"],
  setup(_, { emit }) {
    const { t } = useI18n();
    const [
      registerBasicInfoForm,
      {
        getFieldsValue: getFieldsBasicInfoValue,
        validate: basicInfoValidate,
        setFieldsValue: setBasicInfoFieldsValue
      }
    ] = useForm({
      labelWidth: 120,
      schemas: editFormSchema,
      showActionButtonGroup: false
    });

    let apiScopes: StringStringFromSelector[] = [];
    const state = reactive({
      defaultApiScope: [],
      apiScopes
    });
    const [registerModal, { changeOkLoading, closeModal }] = useModalInner(async (data) => {
      await setBasicInfoFieldsValue({
        name: data.record.name,
        displayName: data.record.displayName,
        secret: data.record.secrets.length > 0 ? data.record.secrets[0].value : "",
        description: data.record.description,
        enabled: data.record.enabled,
        showInDiscoveryDocument: data.record.showInDiscoveryDocument
      });

      state.apiScopes = await getAllApiScopeAsync();
      state.defaultApiScope = data.record.scopes.map((e) => e.scope);
    });

    const submit = async () => {
      await basicInfoValidate();
      const basicInfo = getFieldsBasicInfoValue();
      const requestScope = { apiScopes: state.defaultApiScope };
      const request = Object.assign(basicInfo, requestScope);
      await updateApiResourceAsync({ request, changeOkLoading, closeModal });
      emit("reload");
    };
    return {
      registerModal,
      registerBasicInfoForm,
      submit,
      t,
      ...toRefs(state)
    };
  }
});
</script>
<style lang="less" scoped>
.ant-checkbox-wrapper + .ant-checkbox-wrapper {
  margin-left: 0;
}
</style>

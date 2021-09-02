<template>
  <BasicModal
    title="编辑ApiResources"
    :width="700"
    :canFullscreen="false"
    @ok="submit"
    @register="registerModal"
    :bodyStyle="{ 'padding-top': '0' }"
    :destroyOnClose="true"
  >
    <div>
      <Tabs>
        <TabPane tab="基本信息" key="1">
          <BasicForm @register="registerBasicInfoForm" />
        </TabPane>
        <TabPane tab="ApiScopes" key="2" forceRender>
          <BasicForm @register="registerApiScopeForm" />
        </TabPane>
      </Tabs>
    </div>
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { Tabs } from 'ant-design-vue';
  import {
    editFormSchema,
    editApiScopeSchema,
    getAllApiScopeAsync,
    updateApiResourceAsync,
  } from './ApiResources';

  export default defineComponent({
    name: 'EditApiResources',
    components: {
      BasicModal,
      BasicForm,
      Tabs,
      TabPane: Tabs.TabPane,
    },
    emits: ['reload'],
    setup(_, { emit }) {
      const [
        registerBasicInfoForm,
        {
          getFieldsValue: getFieldsBasicInfoValue,
          validate: basicInfoValidate,
          setFieldsValue: setBasicInfoFieldsValue,
        },
      ] = useForm({
        labelWidth: 120,
        schemas: editFormSchema,
        showActionButtonGroup: false,
      });
      const [
        registerApiScopeForm,
        {
          getFieldsValue: getFieldsApiScopeValue,
          validate: apiScopeValidate,
          updateSchema: updateApiScopeSchema,
        },
      ] = useForm({
        labelWidth: 120,
        schemas: editApiScopeSchema,
        showActionButtonGroup: false,
      });
      const [registerModal, { changeOkLoading, closeModal }] = useModalInner(async (data) => {
        setBasicInfoFieldsValue({
          name: data.record.name,
          displayName: data.record.displayName,
          secret: data.record.secrets.length > 0 ? data.record.secrets[0].value : '',
          description: data.record.description,
          enabled: data.record.enabled,
          showInDiscoveryDocument: data.record.showInDiscoveryDocument,
        });

        let apiScopes = await getAllApiScopeAsync();
        const defaultApiScopes = data.record.scopes.map((e) => e.scope);
        console.log(defaultApiScopes);
        updateApiScopeSchema([
          {
            field: 'apiScopes',
            defaultValue: defaultApiScopes,
            componentProps: { options: apiScopes },
          },
        ]);
      });

      const submit = async () => {
        await basicInfoValidate();
        await apiScopeValidate();
        var basicInfo = getFieldsBasicInfoValue();
        var apiScope = getFieldsApiScopeValue();
        var request = Object.assign(basicInfo, apiScope);
        await updateApiResourceAsync({ request, changeOkLoading, closeModal });
        emit('reload');
      };
      return {
        registerModal,
        registerBasicInfoForm,
        registerApiScopeForm,
        submit,
      };
    },
  });
</script>
<style lang="less" scoped>
  .ant-checkbox-wrapper + .ant-checkbox-wrapper {
    margin-left: 0px;
  }
</style>

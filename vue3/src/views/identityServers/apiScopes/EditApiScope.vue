<template>
  <BasicModal title="创建ApiScope" :canFullscreen="false" @ok="submit" @cancel="cancel" @register="registerModal">
    <BasicForm @register="registerApiScopeForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent, useContext, defineEmit } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { editFormSchema, editApiScopeAsync } from './ApiScopes';
  import { useI18n } from '/@/hooks/web/useI18n';

  export default defineComponent({
    name: 'EditApiScope',
    components: {
      BasicModal,
      BasicForm,
    },
    setup() {
      // 加载父组件方法
      defineEmit(['reload']);
      const ctx = useContext();

      const { t } = useI18n();
      const [registerApiScopeForm, { getFieldsValue, validate, resetFields, setFieldsValue }] = useForm({
        labelWidth: 120,
        schemas: editFormSchema,
        showActionButtonGroup: false,
      });

      const [registerModal, { changeOkLoading, closeModal }] = useModalInner((data) => {
        console.log(data.record);
        setFieldsValue({
          name: data.record.name,
          enabled: data.record.enabled,
          displayName: data.record.displayName,
          description: data.record.description,
          required: data.record.required,
          emphasize: data.record.emphasize,
          showInDiscoveryDocument: data.record.showInDiscoveryDocument,
        });
      });

      // 保存角色
      const submit = async () => {
        try {
          const request = getFieldsValue();
          await editApiScopeAsync({ request, changeOkLoading, validate, closeModal });
          resetFields();
          ctx.emit('reload');
        } catch (error) {
          changeOkLoading(false);
        }
      };

      const cancel = () => {
        resetFields();
        closeModal();
      };
      return {
        t,
        registerModal,
        registerApiScopeForm,
        submit,
        cancel,
      };
    },
  });
</script>

<style lang="less" scoped></style>

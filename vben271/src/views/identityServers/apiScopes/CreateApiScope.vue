<template>
  <BasicModal title="创建ApiScope" :canFullscreen="false" @ok="submit" @cancel="cancel" @register="registerModal">
    <BasicForm @register="registerApiScopeForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent, useContext, defineEmit } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { createFormSchema, createApiScopeAsync } from './ApiScopes';
  import { useI18n } from '/@/hooks/web/useI18n';

  export default defineComponent({
    name: 'CreateApiScope',
    components: {
      BasicModal,
      BasicForm,
    },
    emits: ['reload'],
    setup(_, { emit }) {
      // 加载父组件方法
      // defineEmit(['reload']);
      // const ctx = useContext();

      const { t } = useI18n();
      const [registerApiScopeForm, { getFieldsValue, validate, resetFields }] = useForm({
        labelWidth: 120,
        schemas: createFormSchema,
        showActionButtonGroup: false,
      });

      const [registerModal, { changeOkLoading, closeModal }] = useModalInner();

      const submit = async () => {
        try {
          const request = getFieldsValue();
          await createApiScopeAsync({ request, changeOkLoading, validate, closeModal });
          resetFields();
          emit('reload');
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

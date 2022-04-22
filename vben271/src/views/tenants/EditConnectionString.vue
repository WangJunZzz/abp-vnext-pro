<template>
  <BasicModal
    :title="t('common.editText')"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerModal"
    :minHeight="100"
  >
    <BasicForm @register="registerApiScopeForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import {
    updateConnectionStringFormSchema,
    updateConnectionStringAsync,
    getConnectionStringAsync,
  } from '/@/views/tenants/Tenant';
  import { useI18n } from '/@/hooks/web/useI18n';

  export default defineComponent({
    name: 'EditConnectionString',
    components: {
      BasicModal,
      BasicForm,
    },
    emits: ['reload', 'register'],
    setup(_, { emit }) {
      const { t } = useI18n();
      const [registerApiScopeForm, { getFieldsValue, resetFields, setFieldsValue }] = useForm({
        labelWidth: 120,
        schemas: updateConnectionStringFormSchema,
        showActionButtonGroup: false,
      });

      const [registerModal, { changeOkLoading, closeModal }] = useModalInner(async (data) => {
        const connectionString = await getConnectionStringAsync({ id: data.record.id });
        await setFieldsValue({
          id: data.record.id,
          connectionString: connectionString,
        });
      });

      const submit = async () => {
        try {
          const request = getFieldsValue();
          changeOkLoading(true);
          await updateConnectionStringAsync({ request });
          await resetFields();
          emit('reload');
        } finally {
          changeOkLoading(false);
          closeModal();
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

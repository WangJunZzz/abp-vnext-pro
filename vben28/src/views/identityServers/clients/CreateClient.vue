<template>
  <BasicModal
    :title="t('common.createText')"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerModal"
  >
    <BasicForm @register="registerClientForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { createFormSchema, createClientAsync } from '/@/views/identityServers/clients/Clients';
  import { useI18n } from '/@/hooks/web/useI18n';

  export default defineComponent({
    name: 'CreateAbpRole',
    components: {
      BasicModal,
      BasicForm,
    },
    emits: ['reload', 'register'],
    setup(_, { emit }) {
      const { t } = useI18n();
      const [registerClientForm, { getFieldsValue, validate, resetFields }] = useForm({
        labelWidth: 120,
        schemas: createFormSchema,
        showActionButtonGroup: false,
      });

      const [registerModal, { changeOkLoading, closeModal }] = useModalInner();

      // 保存角色
      const submit = async () => {
        try {
          const request = getFieldsValue();
          await createClientAsync({ request, changeOkLoading, validate, closeModal });
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
        registerClientForm,
        submit,
        cancel,
      };
    },
  });
</script>

<style lang="less" scoped></style>

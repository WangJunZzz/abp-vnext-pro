<template>
  <BasicModal
    :title="t('common.createText')"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerNotificationModal"
  >
    <BasicForm @register="registerNotificationForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { createFormSchema, sendNotificationAsync } from './Index';
  import { useI18n } from '/@/hooks/web/useI18n';
  export default defineComponent({
    name: 'CreateSubscription',
    components: {
      BasicModal,
      BasicForm,
    },
    emits: ['reload', 'register'],
    setup(_, { emit }) {
      const { t } = useI18n();
      const [registerNotificationForm, { getFieldsValue, resetFields, validate }] = useForm({
        labelWidth: 120,
        schemas: createFormSchema,
        showActionButtonGroup: false,
      });

      const [registerNotificationModal, { changeOkLoading, closeModal }] = useModalInner();

      const submit = async () => {
        try {
          const params = getFieldsValue();
          changeOkLoading(true);
          await validate();
          await sendNotificationAsync({ params });
          await resetFields();
          emit('reload');
          closeModal();
        } finally {
          changeOkLoading(false);
        }
      };

      const cancel = () => {
        resetFields();
        closeModal();
      };
      return {
        registerNotificationModal,
        registerNotificationForm,
        submit,
        cancel,
        t
      };
    },
  });
</script>

<style lang="less" scoped></style>

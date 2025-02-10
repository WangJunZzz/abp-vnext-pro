<template>
  <BasicModal
    :title="t('common.createText')"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerLanguageTextModal"
  >
    <BasicForm @register="registerLanguageTextForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { createFormSchema, createAsync } from './Index';
  import { useI18n } from '/@/hooks/web/useI18n';
  export default defineComponent({
    name: 'CreateLanguageText',
    components: {
      BasicModal,
      BasicForm,
    },
    emits: ['reload', 'register'],
    setup(_, { emit }) {
      const { t } = useI18n();
      const [registerLanguageTextForm, { getFieldsValue, resetFields, validate }] = useForm({
        labelWidth: 120,
        schemas: createFormSchema,
        showActionButtonGroup: false,
      });

      const [registerLanguageTextModal, { changeOkLoading, closeModal }] = useModalInner();

      const submit = async () => {
        try {
          const params = getFieldsValue();
          changeOkLoading(true);
          await validate();
          await createAsync({ params });
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
        registerLanguageTextModal,
        registerLanguageTextForm,
        submit,
        cancel,
        t,
      };
    },
  });
</script>

<style lang="less" scoped></style>

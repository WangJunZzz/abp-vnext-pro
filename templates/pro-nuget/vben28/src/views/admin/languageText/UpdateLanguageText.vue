<template>
  <BasicModal
    :title="t('common.editText')"
    :canFullscreen="false"
    @ok="submit"
    @register="registerLanguageTextModal"
  >
    <BasicForm @register="registerLanguageTextForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { updateFormSchema, updateAsync } from './Index';
  import { useI18n } from '/@/hooks/web/useI18n';
  export default defineComponent({
    name: 'UpdateLanguageText',
    components: {
      BasicModal,
      BasicForm,
    },
    emits: ['reload', 'register'],
    setup(_, { emit }) {
      const { t } = useI18n();
      const [registerLanguageTextForm, { getFieldsValue, setFieldsValue }] = useForm({
        labelWidth: 120,
        schemas: updateFormSchema,
        showActionButtonGroup: false,
      });
      const [registerLanguageTextModal, { changeOkLoading, closeModal }] = useModalInner((data) => {
        setFieldsValue({
          resourceName: data.record.resourceName,
          cultureName: data.cultureName,
          name: data.record.name,
          value: data.record.value,
        });
      });

      const submit = async () => {
        try {
          const params = getFieldsValue();
          changeOkLoading(true);
          await updateAsync({ params });
          closeModal();
          emit('reload');
        } finally {
          changeOkLoading(false);
        }
      };

      return {
        registerLanguageTextModal,
        registerLanguageTextForm,
        submit,
        t,
      };
    },
  });
</script>

<style lang="less" scoped></style>

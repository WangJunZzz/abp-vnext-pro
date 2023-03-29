<template>
  <BasicModal
    :title="t('common.editText')"
    :canFullscreen="false"
    @ok="submit"
    @register="registerLanguageModal"
  >
    <BasicForm @register="registerLanguageForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { updateFormSchema, updateAsync } from './Index';
  import { useI18n } from '/@/hooks/web/useI18n';
  export default defineComponent({
    name: 'UpdateLanguage',
    components: {
      BasicModal,
      BasicForm,
    },
    emits: ['reload', 'register'],
    setup(_, { emit }) {
      const { t } = useI18n();
      const [registerLanguageForm, { getFieldsValue, setFieldsValue }] = useForm({
        labelWidth: 120,
        schemas: updateFormSchema,
        showActionButtonGroup: false,
      });
      const [registerLanguageModal, { changeOkLoading, closeModal }] = useModalInner((data) => {
        setFieldsValue({
          id: data.record.id,
          cultureName: data.record.cultureName,
          uiCultureName: data.record.uiCultureName,
          displayName: data.record.displayName,
          flagIcon: data.record.flagIcon,
          isEnabled: data.record.isEnabled,
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
        registerLanguageModal,
        registerLanguageForm,
        submit,
        t,
      };
    },
  });
</script>

<style lang="less" scoped></style>

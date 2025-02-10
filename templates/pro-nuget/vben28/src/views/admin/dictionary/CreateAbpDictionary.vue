<template>
  <BasicModal
    :title="t('common.createText')"
    :canFullscreen="false"
    @ok="submit"
    :maskClosable="false"
    @cancel="cancel"
    @register="registerModal"
    :height="380"
    :destroyOnClose="true"
  >
    <BasicForm @register="registerDictionaryForm" />
  </BasicModal>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { BasicModal, useModalInner } from "/@/components/Modal";
import { BasicForm, useForm } from "/@/components/Form/index";
import { useI18n } from "/@/hooks/web/useI18n";
import { createFormSchema, createDetailsDictionaryAsync } from "/@/views/admin/dictionary/AbpDictionary";

export default defineComponent({
  name: "CreateDictionary",
  components: {
    BasicModal,
    BasicForm
  },

  setup(_, { emit }) {
    const { t } = useI18n();
    const [registerModal, { closeModal, changeOkLoading }] = useModalInner((data) => {
      setFieldsValue({
        id: data.dictionaryCreate.id,
        typeDisplayText: data.dictionaryCreate.displayText
      });
    });
    const [registerDictionaryForm, { resetFields, getFieldsValue, validate, setFieldsValue }] =
      useForm({
        labelWidth: 120,
        schemas: createFormSchema,
        showActionButtonGroup: false
      });
    const submit = async () => {
      try {
        let request = getFieldsValue();
        await createDetailsDictionaryAsync({
          request,
          changeOkLoading,
          validate,
          resetFields,
          closeModal
        });
        emit("reload");
      } catch (error) {
        changeOkLoading(false);
      }
    };

    const cancel = () => {
      resetFields();
      // emit('clearSelectedRowKeys');
      closeModal();
    };

    return {
      registerModal,
      registerDictionaryForm,
      submit,
      t,
      cancel
    };
  }
});
</script>

<style lang="less" scoped></style>

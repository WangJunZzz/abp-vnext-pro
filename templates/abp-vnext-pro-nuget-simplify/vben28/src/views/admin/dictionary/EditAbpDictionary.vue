<template>
  <BasicModal
    :title="t('common.editText')"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerModal"
    :height="380"
    :destroyOnClose="true"
    :maskClosable="false"
  >
    <BasicForm @register="registerDictionaryForm" />
  </BasicModal>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { BasicModal, useModalInner } from "/@/components/Modal";
import { useI18n } from "/@/hooks/web/useI18n";
import { BasicForm, useForm } from "/@/components/Form/index";
import { editFormSchema, editDetailsDictionaryAsync } from "/@/views/admin/dictionary/AbpDictionary";

export default defineComponent({
  name: "EditDictionary",
  components: {
    BasicModal,
    BasicForm
  },
  setup(_, { emit }) {
    const { t } = useI18n();

    const [registerModal, { closeModal, changeOkLoading }] = useModalInner((data) => {
      setFieldsValue({
        id: data.record.id,
        dataDictionaryId: data.record.dataDictionaryId,
        displayText: data.record.displayText,
        description: data.record.description,
        code: data.record.code,
        order: data.record.order
      });
    });
    const [registerDictionaryForm, { setFieldsValue, getFieldsValue, validate, resetFields }] =
      useForm({
        labelWidth: 120,
        schemas: editFormSchema,
        showActionButtonGroup: false
      });
    const submit = async () => {
      try {
        let request = getFieldsValue();
        await editDetailsDictionaryAsync({ request, changeOkLoading, validate, closeModal });
        emit("reload");
      } catch (error) {
        changeOkLoading(false);
      }
    };

    const cancel = () => {
      resetFields();
      closeModal();
    };

    return {
      registerModal,
      registerDictionaryForm,
      submit,
      cancel,
      t
    };
  }
});
</script>

<style lang="less" scoped></style>

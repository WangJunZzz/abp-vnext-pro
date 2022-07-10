<template>
  <BasicModal
    :title="t('common.editText')"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerModal"
    :destroyOnClose="true"
    :maskClosable="false"
    :minHeight="100"
  >
    <BasicForm @register="registerDictionaryTypeForm" />
  </BasicModal>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useI18n } from "/@/hooks/web/useI18n";
import { BasicModal, useModalInner } from "/@/components/Modal";
import { BasicForm, useForm } from "/@/components/Form/index";
import { editDictionaryTypeFormSchema, editDictionaryTypeAsync } from "/@/views/admin/dictionary/AbpDictionary";

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
        code: data.record.code,
        description: data.record.description,
        displayText: data.record.displayText,
        key: data.record.key
      });
    });
    const [registerDictionaryTypeForm, { setFieldsValue, getFieldsValue, validate, resetFields }] =
      useForm({
        labelWidth: 100,
        schemas: editDictionaryTypeFormSchema,
        showActionButtonGroup: false
      });

    const submit = async () => {
      try {
        let request = getFieldsValue();
        await editDictionaryTypeAsync({ request, changeOkLoading, validate, closeModal });
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
      registerDictionaryTypeForm,
      submit,
      t,
      cancel
    };
  }
});
</script>

<style lang="less" scoped></style>

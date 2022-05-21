<template>
  <BasicModal
    :title="t('common.createText')"
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
import { createDictionaryTypeFormSchema, createDictionaryTypeAsync } from "/@/views/admin/dictionary/AbpDictionary";

export default defineComponent({
  name: "CreateAbpDictionaryType",
  components: {
    BasicModal,
    BasicForm
  },
  setup(_, { emit }) {
    const { t } = useI18n();

    const [registerModal, { closeModal, changeOkLoading }] = useModalInner();
    const [registerDictionaryTypeForm, { resetFields, getFieldsValue, validate }] = useForm({
      labelWidth: 100,
      schemas: createDictionaryTypeFormSchema,
      showActionButtonGroup: false
    });
    const submit = async () => {
      try {
        let request = getFieldsValue();
        await createDictionaryTypeAsync({
          request,
          changeOkLoading,
          closeModal,
          validate,
          resetFields
        });
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

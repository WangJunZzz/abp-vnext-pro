<template>
  <div>
    <BasicModal
      :title="t('common.editText')"
      :canFullscreen="false"
      @ok="submit"
      :maskClosable="false"
      @cancel="cancel"
      @register="registerModal"
      :minHeight="100"
      :destroyOnClose="true"
    >
      <BasicForm @register="registerOrganizationUnitForm" />
    </BasicModal>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { BasicModal, useModalInner } from "/@/components/Modal";
import { BasicForm, useForm } from "/@/components/Form/index";
import { useI18n } from "/@/hooks/web/useI18n";
import { editOrganizationUnitFormSchema, editOrganizationUnitAsync } from "/@/views/admin/organizationUnits/OrganizationUnit";

export default defineComponent({
  name: "EditOrganizationUnit",
  components: {
    BasicModal,
    BasicForm
  },

  setup(_, { emit }) {
    const { t } = useI18n();
    const [registerModal, { closeModal, changeOkLoading }] = useModalInner((data) => {
      setFieldsValue({
        displayName: data.record.displayName,
        id: data.record.id
      });
    });
    const [registerOrganizationUnitForm, { getFieldsValue, validate, setFieldsValue }] = useForm({
      labelWidth: 120,
      schemas: editOrganizationUnitFormSchema,
      showActionButtonGroup: false
    });
    const submit = async () => {
      try {
        let request = getFieldsValue();
        await editOrganizationUnitAsync({
          request,
          changeOkLoading,
          validate,
          closeModal
        });
        emit("reload");
      } catch (error) {
        changeOkLoading(false);
      }
    };

    const cancel = () => {
      closeModal();
    };

    return {
      registerModal,
      registerOrganizationUnitForm,
      submit,
      t,
      cancel
    };
  }
});
</script>

<style lang="less" scoped></style>

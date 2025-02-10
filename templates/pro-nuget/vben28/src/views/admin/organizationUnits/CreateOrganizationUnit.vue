<template>
  <div>
    <BasicModal
      :title="t('common.createText')"
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
import {
  createOrganizationUnitFormSchema,
  createOrganizationUnitAsync
} from "/@/views/admin/organizationUnits/OrganizationUnit";

export default defineComponent({
  name: "CreateOrganizationUnit",
  components: {
    BasicModal,
    BasicForm
  },

  setup(_, { emit }) {
    const { t } = useI18n();
    const [registerModal, { closeModal, changeOkLoading }] = useModalInner((data) => {
      if (data.record.parentId == "") {
        updateSchema({ field: "parentDisplayName", ifShow: false });
      } else {
        updateSchema({ field: "parentDisplayName", ifShow: true });
        setFieldsValue({
          parentDisplayName: data.record.parentDisplayName,
          parentId: data.record.parentId
        });
      }
    });
    const [
      registerOrganizationUnitForm,
      { resetFields, getFieldsValue, validate, setFieldsValue, updateSchema }
    ] = useForm({
      labelWidth: 120,
      schemas: createOrganizationUnitFormSchema,
      showActionButtonGroup: false
    });
    const submit = async () => {
      try {
        let request = getFieldsValue();
        await createOrganizationUnitAsync({
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
      registerOrganizationUnitForm,
      submit,
      t,
      cancel
    };
  }
});
</script>

<style lang="less" scoped></style>

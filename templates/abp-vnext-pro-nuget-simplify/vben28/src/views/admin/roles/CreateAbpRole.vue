<template>
  <BasicModal
    :title="t('routes.admin.roleManagement_create_role')"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerModal"
  >
    <BasicForm @register="registerUserForm" />
  </BasicModal>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { BasicModal, useModalInner } from "/@/components/Modal";
import { BasicForm, useForm } from "/@/components/Form/index";
import { createFormSchema, createRoleAsync } from "/@/views/admin/roles/AbpRole";
import { useI18n } from "/@/hooks/web/useI18n";

export default defineComponent({
  name: "CreateAbpRole",
  components: {
    BasicModal,
    BasicForm
  },
  emits: ["reload", "register"],
  setup(_, { emit }) {
    // 加载父组件方法
    // defineEmit(['reload']);
    // const ctx = useContext();

    const { t } = useI18n();
    const [registerUserForm, { getFieldsValue, validate, resetFields }] = useForm({
      labelWidth: 120,
      schemas: createFormSchema,
      showActionButtonGroup: false
    });

    const [registerModal, { changeOkLoading, closeModal }] = useModalInner();

    // 保存角色
    const submit = async () => {
      try {
        const request = getFieldsValue();
        await createRoleAsync({ request, changeOkLoading, validate, closeModal });
        await resetFields();
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
      t,
      registerModal,
      registerUserForm,
      submit,
      cancel
    };
  }
});
</script>

<style lang="less" scoped></style>

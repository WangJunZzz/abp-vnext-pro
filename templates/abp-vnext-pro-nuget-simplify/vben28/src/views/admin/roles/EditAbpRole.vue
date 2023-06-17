<template>
  <BasicModal
    :title="t('routes.admin.roleManagement_edit')"
    :canFullscreen="false"
    @ok="submit"
    @register="registerModal"
  >
    <BasicForm @register="registerUserForm" />
  </BasicModal>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { BasicModal, useModalInner } from "/@/components/Modal";
import { BasicForm, useForm } from "/@/components/Form/index";
import { editFormSchema, updateRoleAsync } from "/@/views/admin/roles/AbpRole";
import { useI18n } from "/@/hooks/web/useI18n";
import {
  IdentityRoleDto,
  UpdateRoleInput,
  IdentityRoleUpdateDto
} from "/@/services/ServiceProxies";

export default defineComponent({
  name: "EditAbpRole",
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
    const [registerUserForm, { getFieldsValue, validate, setFieldsValue }] = useForm({
      labelWidth: 120,
      schemas: editFormSchema,
      showActionButtonGroup: false
    });
    let currentRoleInfo = new IdentityRoleDto();
    const [registerModal, { changeOkLoading, closeModal }] = useModalInner((data) => {
      currentRoleInfo = data.record;
      setFieldsValue({
        name: data.record.name,
        isDefault: data.record.isDefault ? "1" : "0"
      });
    });

    // 保存角色
    const submit = async () => {
      try {
        const request = getFieldsValue();
        let role = new UpdateRoleInput();
        role.roleId = currentRoleInfo.id;
        role.roleInfo = new IdentityRoleUpdateDto();
        role.roleInfo.name = request.name;
        role.roleInfo.isDefault = request.isDefault == 1;
        role.roleInfo.isPublic = currentRoleInfo.isPublic;
        role.roleInfo.concurrencyStamp = currentRoleInfo.concurrencyStamp;
        await updateRoleAsync({ request: role, changeOkLoading, validate, closeModal });
        emit("reload");
      } catch (error) {
        changeOkLoading(false);
      }
    };

    return {
      t,
      registerModal,
      registerUserForm,
      submit
    };
  }
});
</script>

<style lang="less" scoped></style>

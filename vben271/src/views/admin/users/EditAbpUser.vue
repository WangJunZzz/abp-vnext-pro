<template>
  <BasicModal
    :title="t('routes.admin.userManagement_edit_user')"
    :width="600"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerModal"
    @visible-change="visibleChange"
    :bodyStyle="{ 'padding-top': '0' }"
    :destroyOnClose="true"
    :maskClosable="false"
  >
    <div>
      <Tabs>
        <TabPane :tab="t('routes.admin.userManagement_userInfo')" key="1">
          <BasicForm @register="registerUserForm" />
        </TabPane>
        <TabPane :tab="t('routes.admin.userManagement_role')" key="2">
          <a-checkbox-group v-model:value="defaultRolesRef">
            <a-row justify="center">
              <a-col :span="24">
                <a-checkbox
                  style="width: 150px"
                  v-for="(item, index) in rolesRef"
                  :key="index"
                  :value="item.name"
                >
                  {{ item.name }}
                </a-checkbox
                >
              </a-col>
            </a-row>
          </a-checkbox-group>
        </TabPane>
      </Tabs>
    </div>
  </BasicModal>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { BasicModal, useModalInner } from "/@/components/Modal";
import { BasicForm, useForm } from "/@/components/Form/index";
import { Tabs } from "ant-design-vue";
import {
  editFormSchema,
  getAllRoleAsync,
  updateUserAsync,
  getRolesByUserIdAsync
} from "/@/views/admin/users/AbpUser";
import {
  IdentityRoleDto,
  IdentityUserDto,
  UpdateUserInput,
  IdentityUserUpdateDto
} from "/@/services/ServiceProxies";
import { message } from "ant-design-vue";
import { useI18n } from "/@/hooks/web/useI18n";

export default defineComponent({
  name: "EditAbpUser",
  components: {
    BasicModal,
    BasicForm,
    Tabs,
    TabPane: Tabs.TabPane
  },
  emits: ["reload", "register"],
  setup(_, { emit }) {
    const [registerUserForm, { getFieldsValue, validate, setFieldsValue, resetFields }] = useForm(
      {
        labelWidth: 120,
        schemas: editFormSchema,
        showActionButtonGroup: false
      }
    );
    const { t } = useI18n();
    let currentUserInfo = new IdentityUserDto();
    const [registerModal, { changeOkLoading, closeModal }] = useModalInner((data) => {
      currentUserInfo = data.record;
      setFieldsValue({
        name: data.record.name,
        userName: data.record.userName,
        email: data.record.email,
        phoneNumber: data.record.phoneNumber
      });
    });

    let roles: IdentityRoleDto[] = [];
    let defaultRoles: string[] = [];
    let rolesRef = ref(roles);
    let defaultRolesRef = ref(defaultRoles);

    const visibleChange = async (visible: boolean) => {
      if (visible) {
        const roles = await getAllRoleAsync();
        const userRoles = await getRolesByUserIdAsync(currentUserInfo.id as string);
        userRoles.items?.forEach((e) => {
          defaultRolesRef.value.push(e.name as string);
        });
        roles.items?.forEach((e) => {
          rolesRef.value.push(e);
        });
      } else {
        rolesRef.value.splice(0, rolesRef.value.length);
        defaultRolesRef.value.splice(0, defaultRolesRef.value.length);
      }
    };

    const submit = async () => {
      try {
        let request = getFieldsValue();

        if (request.password != request.confirmPassword) {
          message.error(t("routes.admin.editPasswordMessage"));
        }
        let updateUserInput = new UpdateUserInput();
        let userInfo = new IdentityUserUpdateDto();

        userInfo.userName = request.userName;
        userInfo.name = request.name;
        userInfo.surname = currentUserInfo.surname;
        userInfo.email = request.email;
        userInfo.phoneNumber = request.phoneNumber;
        userInfo.lockoutEnabled = currentUserInfo.lockoutEnabled;
        userInfo.concurrencyStamp = currentUserInfo.concurrencyStamp;
        userInfo.roleNames = defaultRolesRef.value;
        userInfo.password = request.password;

        updateUserInput.userId = currentUserInfo.id;
        updateUserInput.userInfo = userInfo;
        await updateUserAsync({
          request: updateUserInput,
          changeOkLoading,
          validate,
          closeModal,
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
      registerUserForm,
      submit,
      rolesRef,
      visibleChange,
      defaultRolesRef,
      cancel,
      t
    };
  }
});
</script>
<style lang="less" scoped>
.ant-checkbox-wrapper + .ant-checkbox-wrapper {
  margin-left: 0;
}
</style>

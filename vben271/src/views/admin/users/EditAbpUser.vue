<template>
  <BasicModal
    title="编辑用户"
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
        <TabPane tab="用户信息" key="1">
          <BasicForm @register="registerUserForm" />
        </TabPane>
        <TabPane tab="角色" key="2">
          <!-- <a-checkbox-group @change="onRoleSelectedChange" v-model:value="defaultRolesRef">
            <a-checkbox v-for="(item, index) in rolesRef" :key="index" :value="item.name">
              {{ item.name }}
            </a-checkbox>
          </a-checkbox-group> -->

          <a-checkbox-group v-model:value="defaultRolesRef">
            <a-row justify="center">
              <a-col :span="24">
                <a-checkbox
                  style="width: 150px"
                  v-for="(item, index) in rolesRef"
                  :key="index"
                  :value="item.name"
                  >{{ item.name }}</a-checkbox
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
  import { defineComponent, ref } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { Tabs } from 'ant-design-vue';
  import {
    editFormSchema,
    getAllRoleAsync,
    updateUserAsync,
    getRolesByUserIdAsync,
  } from './AbpUser';
  import {
    IdentityRoleDto,
    IdentityUserDto,
    UpdateUserInput,
    IdentityUserUpdateDto,
  } from '/@/services/ServiceProxies';
  import { message } from 'ant-design-vue';
  export default defineComponent({
    name: 'EditAbpUser',
    components: {
      BasicModal,
      BasicForm,
      Tabs,
      TabPane: Tabs.TabPane,
    },
    emits: ['reload'],
    setup(_, { emit }) {
      const [registerUserForm, { getFieldsValue, validate, setFieldsValue, resetFields }] = useForm(
        {
          labelWidth: 120,
          schemas: editFormSchema,
          showActionButtonGroup: false,
        }
      );
      let currentUserInfo = new IdentityUserDto();
      const [registerModal, { changeOkLoading, closeModal }] = useModalInner((data) => {
        currentUserInfo = data.record;
        setFieldsValue({
          name: data.record.name,
          userName: data.record.userName,
          email: data.record.email,
          phoneNumber: data.record.phoneNumber,
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

      // // 选择角色
      // const onRoleSelectedChange = (value: string[]) => {
      //   defaultRolesRef.splice(0, defaultRolesRef.length);
      //   value.forEach((e) => {
      //     defaultRolesRef.push(e);
      //   });
      // };

      const submit = async () => {
        try {
          let request = getFieldsValue();

          if (request.password != request.confirmPassword) {
            message.error('两次密码输入不一致');
            throw new Error('两次密码输入不一致');
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
            resetFields,
          });
          emit('reload');
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
      };
    },
  });
</script>
<style lang="less" scoped>
  .ant-checkbox-wrapper + .ant-checkbox-wrapper {
    margin-left: 0px;
  }
</style>

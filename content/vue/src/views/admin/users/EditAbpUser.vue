<template>
  <BasicModal
    title="编辑用户"
    :canFullscreen="false"
    @ok="submit"
    @register="registerModal"
    @visible-change="visibleChange"
    :bodyStyle="{ 'padding-top': '0' }"
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

          <a-checkbox-group v-model:value="defaultRolesRef" @change="onRoleSelectedChange">
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
  import { defineComponent, reactive, useContext, defineEmit } from 'vue';
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
    IdentityUserUpdateDto,
  } from '/@/services/ServiceProxies';

  export default defineComponent({
    name: 'EditAbpUser',
    components: {
      BasicModal,
      BasicForm,
      Tabs,
      TabPane: Tabs.TabPane,
    },
    setup() {
      // 加载父组件方法
      defineEmit(['reload']);
      const ctx = useContext();

      const [registerUserForm, { getFieldsValue, validate, setFieldsValue }] = useForm({
        labelWidth: 120,
        schemas: editFormSchema,
        showActionButtonGroup: false,
      });
      let currentUserInfo = new IdentityUserDto();
      const [registerModal, { changeOkLoading, closeModal }] = useModalInner((data) => {
        currentUserInfo = data.record;
        setFieldsValue({
          name: data.record.name,
          userName: data.record.userName,
          email: data.record.email,
        });
      });

      let roles: IdentityRoleDto[] = [];
      let defaultRoles: string[] = [];
      let rolesRef = reactive(roles);
      let defaultRolesRef = reactive(defaultRoles);

      const visibleChange = async (visible: boolean) => {
        if (visible) {
          const roles = await getAllRoleAsync();
          const userRoles = await getRolesByUserIdAsync(currentUserInfo.id as string);
          userRoles.items?.forEach((e) => {
            defaultRolesRef.push(e.name as string);
          });
          roles.items?.forEach((e) => {
            rolesRef.push(e);
          });
        } else {
          rolesRef.splice(0, rolesRef.length);
          defaultRolesRef.splice(0, defaultRolesRef.length);
        }
      };

      // 选择角色
      const onRoleSelectedChange = (value: string[]) => {
        defaultRolesRef.splice(0, defaultRolesRef.length);
        value.forEach((e) => {
          defaultRolesRef.push(e);
        });
      };

      const submit = async () => {
        try {
          let request = getFieldsValue();
          let userInfo = new IdentityUserUpdateDto();
          request.userId = currentUserInfo.id;
          userInfo.userName = request.userName;
          userInfo.name = request.name;
          userInfo.surname = currentUserInfo.surname;
          userInfo.email = request.email;
          userInfo.phoneNumber = currentUserInfo.phoneNumber;
          userInfo.lockoutEnabled = currentUserInfo.lockoutEnabled;
          userInfo.concurrencyStamp = currentUserInfo.concurrencyStamp;
          userInfo.roleNames = defaultRolesRef;
          request.userInfo = userInfo;
          await updateUserAsync({ request, changeOkLoading, validate, closeModal });
          ctx.emit('reload');
        } catch (error) {
          changeOkLoading(false);
        }
      };

      return {
        registerModal,
        registerUserForm,
        submit,
        rolesRef,
        onRoleSelectedChange,
        visibleChange,
        defaultRolesRef,
      };
    },
  });
</script>
<style lang="less" scoped>
  .ant-checkbox-wrapper + .ant-checkbox-wrapper {
    margin-left: 0px;
  }
</style>

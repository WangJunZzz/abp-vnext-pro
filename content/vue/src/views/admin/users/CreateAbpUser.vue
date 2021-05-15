<template>
  <BasicModal
    :title="t('routes.admin.userManagement_create_user')"
    :canFullscreen="false"
    @ok="submit"
    @register="registerModal"
    @visible-change="visibleChange"
    :bodyStyle="{ 'padding-top': '0' }"
  >
    <div style="padding: 0">
      <Tabs>
        <TabPane :tab="t('routes.admin.userManagement_userInfo')" key="1">
          <BasicForm @register="registerUserForm" />
        </TabPane>
        <TabPane :tab="t('routes.admin.userManagement_role')" key="2">
          <!-- <a-checkbox-group @change="onRoleSelectedChange" v-model:value="defaultRolesRef">
            <a-checkbox v-for="(item, index) in rolesRef" :key="index" :value="item.name">
              {{ item.name }}
            </a-checkbox>
          </a-checkbox-group> -->

          <a-checkbox-group @change="onRoleSelectedChange" v-model:value="defaultRolesRef">
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
  import { Tabs } from 'ant-design-vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { createFormSchema, getAllRoleAsync, createUserAsync } from './AbpUser';
  import { IdentityRoleDto, IdentityUserCreateDto } from '/@/services/ServiceProxies';
  import { useI18n } from '/@/hooks/web/useI18n';
  export default defineComponent({
    name: 'CreateAbpUser',
    components: {
      BasicModal,
      Tabs,
      TabPane: Tabs.TabPane,
      BasicForm,
    },
    setup() {
      // 加载父组件方法
      defineEmit(['reload']);
      const ctx = useContext();

      const { t } = useI18n();
      const [registerModal, { changeOkLoading, closeModal }] = useModalInner();
      const [registerUserForm, { getFieldsValue, validate, resetFields }] = useForm({
        labelWidth: 120,
        schemas: createFormSchema,
        showActionButtonGroup: false,
      });

      let itemRoles: IdentityRoleDto[] = [];
      let rolesRef = reactive(itemRoles);
      let defaultRoles: string[] = [];
      let defaultRolesRef = reactive(defaultRoles);
      const visibleChange = async (visible: boolean) => {
        if (visible) {
          rolesRef.splice(0, rolesRef.length);
          defaultRolesRef.splice(0, defaultRolesRef.length);
          const roles = await getAllRoleAsync();
          roles.items?.forEach((e) => {
            rolesRef.push(e);
          });
        } else {
          resetFields();
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

      // 保存用户
      const submit = async () => {
        try {
          let request = getFieldsValue() as IdentityUserCreateDto;
          request.roleNames = defaultRolesRef;
          await createUserAsync({
            request,
            changeOkLoading,
            validate,
            closeModal,
            resetFields,
          });
          ctx.emit('reload');
        } catch (error) {
          changeOkLoading(false);
        }
      };
      return {
        t,
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

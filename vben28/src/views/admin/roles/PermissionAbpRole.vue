<template>
  <BasicDrawer @register="registerDrawer" :maskClosable="false" :title="t('routes.admin.roleManagement_permission')" width="20%">
    <BasicTree :treeData="allPermissionsRef"  checkable ref="treeRef" style="margin-bottom: 50px" />
    <div
      :style="{
        position: 'absolute',
        right: 0,
        bottom: 0,
        width: '100%',
        borderTop: '1px solid #e9e9e9',
        padding: '10px 16px',
        background: '#fff',
        textAlign: 'right',
        zIndex: 1,
      }"
    >
      <a-button :style="{ marginRight: '8px' }" @click="closeDrawer">{{ t('common.cancelText') }} </a-button>
      <a-button type="primary" @click="submitRolePermisstionAsync">
        {{ t('common.saveText') }}
      </a-button>
    </div>
  </BasicDrawer>
</template>

<script lang="ts">
import { defineComponent, reactive, ref, unref } from 'vue';
import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
import { getRolePermissionAsync, updateRolePermissionAsync } from './AbpRole';
import { useI18n } from '/@/hooks/web/useI18n';
import { BasicTree, TreeActionType } from '/@/components/Tree/index';
import { useUserStoreWithOut } from '/@/store/modules/user';
import { UpdateRolePermissionsInput, UpdatePermissionDto, UpdatePermissionsDto } from '/@/services/ServiceProxies';
import { message } from 'ant-design-vue';
export default defineComponent({
  name: 'PermissionAbpRole',
  components: { BasicDrawer, BasicTree },
  setup() {
    let roleName: string = '';
    const { t } = useI18n();
    const [registerDrawer, { closeDrawer, setDrawerProps }] = useDrawerInner(async (data) => {
      roleName = data.record.name;
      await getRolePermissions(data.record.name);
      //loading.value = false;
    });
    const treeRef = ref<Nullable<TreeActionType>>(null);

    const totalRolePermissionsRef = reactive([]);
    let allPermissionsRef = reactive([]);
    const getTree = () => {
      const tree = unref(treeRef);
      if (!tree) {
        throw new Error('tree is null!');
      }
      return tree;
    };
    /**
     * 获取权限信息并且构造权限树
     */
    const getRolePermissions = async (roleName: string) => {
      setDrawerProps({ loading: true });
      totalRolePermissionsRef.splice(0, totalRolePermissionsRef.length);
      allPermissionsRef.splice(0, allPermissionsRef.length);
      const permissions = await getRolePermissionAsync(roleName);
      totalRolePermissionsRef.push(...(permissions.allGrants as []));
      allPermissionsRef.push(...(permissions.permissions as []));


     getTree().setCheckedKeys(permissions.grants as []);
      setDrawerProps({ loading: false });
    };

    const submitRolePermisstionAsync = async () => {
      let request: UpdateRolePermissionsInput = new UpdateRolePermissionsInput();
      request.updatePermissionsDto = new UpdatePermissionsDto();
      const keys = getTree().getCheckedKeys();
      let permisstions: UpdatePermissionDto[] = [];
      request.providerName = 'R';
      request.providerKey = roleName;
      totalRolePermissionsRef.forEach((item:string)=>{
         let permisstion = new UpdatePermissionDto();
            permisstion.name = item;
          if((keys as []).some(e=>e==item)){
            permisstion.isGranted = true;
            permisstions.push(permisstion);
          }else{
            permisstion.isGranted = false;
            permisstions.push(permisstion);
          }
      })
      request.updatePermissionsDto.permissions = permisstions;
      await updateRolePermissionAsync({ request, closeDrawer, setDrawerProps });
      const userStore = useUserStoreWithOut();
      if (userStore.getUserInfo.roles.some(e=>e==roleName)) {
        message.success(t('routes.admin.grantedMessage'));
        await userStore.logout(true);
      } else {
        message.success(t('common.operationSuccess'));
      }
    };
    return {
      t,
      registerDrawer,
      allPermissionsRef,
      submitRolePermisstionAsync,
      closeDrawer,
      treeRef,
    };
  },
});
</script>
<style lang="less" scoped></style>

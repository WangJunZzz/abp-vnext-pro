<template>
  <BasicDrawer
    @register="registerDrawer"
    :title="t('routes.admin.roleManagement_permission')"
    width="20%"
  >
    <!-- <a-tree
      checkable
      :tree-data="allPermissionsRef"
      v-model:checkedKeys="currentRolePermissionsRef"
    >
      <template #title0010><span style="color: #1890ff"></span></template>
    </a-tree> -->

    <BasicTree
      :treeData="allPermissionsRef"
      checkable
      v-model:checkedKeys="currentRolePermissionsRef"
    />
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
      <a-button :style="{ marginRight: '8px' }" @click="closeDrawer"
        >{{ t('common.cancelText') }}
      </a-button>
      <a-button type="primary" @click="submitRolePermisstionAsync">
        {{ t('common.saveText') }}
      </a-button>
    </div>
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, reactive, ref } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { getRolePermissionAsync, updateRolePermissionAsync, excludePermission } from './AbpRole';
  import { TreeDataItem } from 'ant-design-vue/es/tree/Tree';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { BasicTree } from '/@/components/Tree/index';
  import { usePermissionStore } from '/@/store/modules/permission';
  import {
    UpdateRolePermissionsDto,
    UpdatePermissionDto,
    UpdatePermissionsDto,
  } from '/@/services/ServiceProxies';

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
      const currentRolePermissionsRef = ref<string[]>([]);
      let allPermissions: TreeDataItem[] = [];
      let allPermissionsRef = reactive(allPermissions);

      /**
       * 获取权限信息并且构造权限树
       */
      const getRolePermissions = async (roleName: string) => {
        setDrawerProps({ loading: true });
        currentRolePermissionsRef.value.splice(0, currentRolePermissionsRef.value.length);
        allPermissionsRef.splice(0, allPermissionsRef.length);
        const permissions = await getRolePermissionAsync(roleName);
        permissions.groups?.forEach((item) => {
          if (excludePermission(item.name as string)) return;
          let temp: TreeDataItem = {
            title: item.displayName,
            key: item.name,
            children: [],
          };
          item.permissions?.forEach((perItem) => {
            if (excludePermission(perItem.name as string)) return;
            let childrenItem: TreeDataItem = {};
            childrenItem.title = perItem.displayName;
            childrenItem.key = perItem.name;
            temp.children?.push(childrenItem);
            if (perItem.isGranted) {
              currentRolePermissionsRef.value.push(perItem.name as string);
            }
          });
          allPermissionsRef.push(temp);
        });

        setDrawerProps({ loading: false });
      };

      const submitRolePermisstionAsync = async () => {
        debugger;
        let request: UpdateRolePermissionsDto = new UpdateRolePermissionsDto();
        request.updatePermissionsDto = new UpdatePermissionsDto();

        let permisstions: UpdatePermissionDto[] = [];
        request.providerName = 'R';
        request.providerKey = roleName;

        currentRolePermissionsRef.value.forEach((item) => {
          if (item.indexOf('.') > 0) {
            let permisstion = new UpdatePermissionDto();
            permisstion.name = item;
            permisstion.isGranted = true;
            permisstions.push(permisstion);
          }
        });
        request.updatePermissionsDto.permissions = permisstions;
        await updateRolePermissionAsync({ request, closeDrawer, setDrawerProps });
        const permissionStore = usePermissionStore();
        const grantPolicy = Object.values(currentRolePermissionsRef.value as object);
        permissionStore.setPermCodeList(grantPolicy);
      };
      return {
        t,
        registerDrawer,
        allPermissionsRef,
        currentRolePermissionsRef,
        submitRolePermisstionAsync,
        closeDrawer,
      };
    },
  });
</script>
<style lang="less" scoped></style>

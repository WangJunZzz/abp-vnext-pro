<template>
  <div>
    <PageWrapper dense contentFullHeight contentClass="flex">
      <div class="bg-white m-4  mr-0 overflow-hidden">
        <BasicTree
          toolbar
          search
          :treeData="treeData"
          :beforeRightClick="getRightMenuList"
          @select="handleSelect"
        >
          <template #headerTitle>
            <span style="font-weight: 500"> {{ t("routes.admin.organizationUnit") }}</span>
            <a-button
              type="primary"
              style="margin-left: 20px"
              size="small"
              v-auth="'AbpIdentity.OrganizationUnitManagement.Create'"
              @click="createRootOrganizationUnit"
            >
              {{ t("routes.admin.createRootOrganizationUnit") }}
            </a-button>
          </template>
        </BasicTree>
      </div>
      <div class="bg-white m-4 p-4 mr-0 w-3/4 xl:w-4/5">
        <a-tabs v-model:activeKey="activeKey" @change="activeKeyChange">
          <a-tab-pane key="1" :tab="t('routes.admin.member')" >
            <BasicTable @register="registerUserTable" size="small">
              <template #toolbar>
                <a-button
                  preIcon="ant-design:plus-circle-outlined"
                  type="primary"
                  v-auth="'AbpIdentity.OrganizationUnitManagement.Update'"
                  @click="openAddUserToOrganizationUnitModal"
                >
                  {{ t("common.createText") }}
                </a-button>
              </template>>
              <template #action="{ record }">
                <TableAction
                  :actions="[
                  {
                    icon: 'ant-design:delete-outlined',
                    label: t('common.delText'),
                    auth:'AbpIdentity.OrganizationUnitManagement.Delete',
                    onClick: handleUserDelete.bind(null, record),
                  },
                 ]"
                />
              </template>

            </BasicTable>
          </a-tab-pane>
          <a-tab-pane key="2" :tab="t('routes.admin.role')" >
            <BasicTable @register="registerRoleTable" size="small">
              <template #toolbar>
                <a-button
                  preIcon="ant-design:plus-circle-outlined"
                  type="primary"
                  v-auth="'AbpIdentity.OrganizationUnitManagement.Update'"
                  @click="openAddRoleToOrganizationUnitModal"
                >
                  {{ t("common.createText") }}
                </a-button>

              </template>
              <template #action="{ record }">
                <TableAction
                  :actions="[
                  {
                    icon: 'ant-design:delete-outlined',
                    label: t('common.delText'),
                    auth:'AbpIdentity.OrganizationUnitManagement.Delete',
                    onClick: handleRoleDelete.bind(null, record),
                  },
                  ]"
                />
              </template>
            </BasicTable>
          </a-tab-pane>
        </a-tabs>
      </div>
    </PageWrapper>
    <CreateOrganizationUnit
      @register="registerCreateOrganizationUnit"
      @reload="initOrganizationUnit"
    />
    <EditOrganizationUnit @register="registerEditOrganizationUnit" @reload="initOrganizationUnit" />
    <AddRoleToOrganizationUnit @register="registerAddRoleToOrganizationUnit"
                               @reload="reloadRole" />
    <AddUserToOrganizationUnit @register="registerAddUserToOrganizationUnit"
                               @reload="reloadUser" />
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from "vue";
import { PageWrapper } from "/@/components/Page";
import { BasicTree, ContextMenuItem } from "/@/components/Tree";
import { BasicTable, useTable, TableAction } from "/@/components/Table";
import {
  getTreeAsync,
  deleteTreeNodeAsync,
  getUserTableListAsync,
  getRoleTableListAsync,
  userTableColumns,
  roleTableColumns,
  searchUserFormSchema,
  removeRoleFromOrganizationUnitAsync,
  removeUserFromOrganizationUnitAsync
} from "/@/views/admin/organizationUnits/OrganizationUnit";
import {
  TreeOutput,
  GetOrganizationUnitUserInput,
  GetOrganizationUnitRoleInput, RemoveUserToOrganizationUnitInput, RemoveRoleToOrganizationUnitInput
} from "/@/services/ServiceProxies";
import { Tabs } from "ant-design-vue";
import CreateOrganizationUnit from "./CreateOrganizationUnit.vue";
import EditOrganizationUnit from "./EditOrganizationUnit.vue";
import AddRoleToOrganizationUnit from "./AddRoleToOrganizationUnit.vue";
import AddUserToOrganizationUnit from "./AddUserToOrganizationUnit.vue";

import { useModal } from "/@/components/Modal";
import { useMessage } from "/@/hooks/web/useMessage";
import { useI18n } from "/@/hooks/web/useI18n";


export default defineComponent({
  name: "OrganizationUnit",
  components: {
    BasicTree,
    PageWrapper,
    Tabs,
    TabPane: Tabs.TabPane,
    CreateOrganizationUnit,
    EditOrganizationUnit,
    BasicTable,
    TableAction,
    AddRoleToOrganizationUnit,
    AddUserToOrganizationUnit
  },
  setup() {
    const { t } = useI18n();
    const { createConfirm } = useMessage();
    const treeData = ref<TreeOutput[]>([]);
    const activeKey = ref("1");
    const [registerCreateOrganizationUnit, { openModal: CreateOrganizationUnitModal }] =useModal();
    const [registerEditOrganizationUnit, { openModal: EditOrganizationUnitModal }] = useModal();
    const [registerAddRoleToOrganizationUnit, { openModal: AddRoleToOrganizationUnitModal }] = useModal();
    const [registerAddUserToOrganizationUnit, { openModal: AddUserToOrganizationUnitModal }] = useModal();

    let organizationUnitId: string = "";
    const openAddUserToOrganizationUnitModal= ()=>{
      if(organizationUnitId)
      {
        AddUserToOrganizationUnitModal(true,{organizationUnitId});
      }
    };
    const openAddRoleToOrganizationUnitModal= ()=>{
      if(organizationUnitId)
      {
        AddRoleToOrganizationUnitModal(true,{organizationUnitId});
      }
    };
    const initOrganizationUnit = async () => {
      treeData.value = await getTreeAsync();
    };
    onMounted(async () => {
      await initOrganizationUnit();
    });

    // 新增根节点
    function createRootOrganizationUnit() {
      let record = {
        parentId: "",
        parentDisplayName: ""
      };
      CreateOrganizationUnitModal(true, { record });
    }

    function getRightMenuList(node: any): ContextMenuItem[] {
    
      return [
        {
          label: t("common.createText"),
          handler: () => {
            let record = {
              parentId: node.eventKey,
              parentDisplayName: node.title.props.name
            };
   
            CreateOrganizationUnitModal(true, { record });
          },
          icon: "bi:plus"
        },
        {
          label: t("common.editText"),
          handler: () => {
            let record = {
              id: node.eventKey,
              displayName: node.title.props.name
            };
            EditOrganizationUnitModal(true, { record });
          },
          icon: "ant-design:edit-outlined"
        },
        {
          label: t("common.delText"),
          handler: () => {
            createConfirm({
              iconType: "warning",
              title: t("common.tip"),
              content: t("common.askDelete"),
              onOk: async () => {
                await deleteTreeNodeAsync({ id: node.eventKey });
                await initOrganizationUnit();
              }
            });
          },
          icon: "ant-design:delete-outlined",
        }
      ];
    }

    async function handleSelect(keys) {
      if (keys.length > 0) {
        organizationUnitId = keys[0];
        if (activeKey.value == "1") {
          await reloadUser();
        } else {
          await reloadRole();
        }
      } else {
        organizationUnitId = "";
      }

    }

    const getUserAsync = async () => {
      if (organizationUnitId) {
        let request = new GetOrganizationUnitUserInput();
        request.filter = getUserForm().getFieldsValue().filter;
        request.organizationUnitId = organizationUnitId;
        return await getUserTableListAsync(request);
      }

    };

    const getRoleAsync = async () => {
      if (organizationUnitId) {
        let request = new GetOrganizationUnitRoleInput();
        request.organizationUnitId = organizationUnitId;
        return await getRoleTableListAsync(request);
      }

    };
    const [registerUserTable, { reload: reloadUser, getForm: getUserForm }] = useTable({
      columns: userTableColumns,
      formConfig: {
        labelWidth: 70,
        schemas: searchUserFormSchema
      },
      api: getUserAsync,
      showTableSetting: true,
      useSearchForm: true,
      bordered: true,
      canResize: true,
      showIndexColumn: true,
      actionColumn: {
        width: 120,
        title: t("common.action"),
        dataIndex: "action",
        slots: {
          customRender: "action"
        },
        fixed: "right"
      }
    });

    const [registerRoleTable, { reload: reloadRole }] = useTable({
      columns: roleTableColumns,
      api: getRoleAsync,
      showTableSetting: true,
      useSearchForm: false,
      bordered: true,
      canResize: true,
      showIndexColumn: true,
      actionColumn: {
        width: 120,
        title: t("common.action"),
        dataIndex: "action",
        slots: {
          customRender: "action"
        },
        fixed: "right"
      }
    });
    const activeKeyChange = async (activeKey) => {

      if (organizationUnitId) {
        if (activeKey == 1) {
          await reloadUser();
        } else {
          await reloadRole();
        }
      }


    };

    const handleUserDelete = async (record: Recordable) => {
      let msg = t("common.askDelete");
      createConfirm({
        iconType: "warning",
        title: t("common.tip"),
        content: msg,
        onOk: async () => {
          let request = new RemoveUserToOrganizationUnitInput();
          request.userId = record.id;
          request.organizationUnitId = organizationUnitId;
          await removeUserFromOrganizationUnitAsync(request);
          await reloadUser();
        }
      });
    };

    const handleRoleDelete = async (record: Recordable) => {
      let msg = t("common.askDelete");
      createConfirm({
        iconType: "warning",
        title: t("common.tip"),
        content: msg,
        onOk: async () => {
          let request = new RemoveRoleToOrganizationUnitInput();
          request.roleId = record.id;
          request.organizationUnitId = organizationUnitId;
          await removeRoleFromOrganizationUnitAsync(request);
          await reloadRole();
        }
      });
    };
    return {
      treeData,
      getRightMenuList,
      createRootOrganizationUnit,
      activeKey,
      registerCreateOrganizationUnit,
      initOrganizationUnit,
      handleSelect,
      registerEditOrganizationUnit,
      registerUserTable,
      registerRoleTable,
      activeKeyChange,
      handleUserDelete,
      handleRoleDelete,
      t,
      registerAddRoleToOrganizationUnit,
      openAddRoleToOrganizationUnitModal,
      getRoleAsync,
      reloadRole,
      reloadUser,
      openAddUserToOrganizationUnitModal,
      registerAddUserToOrganizationUnit,
      getUserAsync
    };
  }
});
</script>
<style scoped>
/*.ant-tabs-tabpane {*/
/*  background: #F0F2F5;*/
/*}*/
</style>

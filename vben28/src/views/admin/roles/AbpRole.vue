<template>
  <div>
    <BasicTable @register="registerTable" size="small">
    <template #bodyCell="{ column, record }">
      <template v-if="column.key === 'isDefault'">
        <Tag :color="record.isDefault ? 'red' : 'green'">
          {{ record.isDefault ? "是" : "否" }}
        </Tag>
      </template>
    </template>
      <template #toolbar>
        <a-button
          type="primary"
          preIcon="ant-design:plus-circle-outlined"
          @click="openCreateAbpRoleModal"
          v-auth="'AbpIdentity.Roles.Create'"
        >
          {{ t("common.createText") }}
        </a-button>
      </template>

      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              auth: 'AbpIdentity.Roles.ManagePermissions',
              label: t('routes.admin.roleManagement_permission'),
              icon: 'ant-design:property-safety-outlined',
              onClick: handlePermission.bind(null, record),
            },
          ]"
          :dropDownActions="[
            {
              auth: 'AbpIdentity.Roles.Update',
              label: t('common.editText'),
              onClick: handleEdit.bind(null, record),
            },
            {
              auth: 'AbpIdentity.Roles.Delete',
              label: t('common.delText'),
              onClick: handleDelete.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>

    <CreateAbpRole
      @register="registerCreateAbpRoleModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />

    <PermissionAbpRole
      @register="registerPermissionAbpRoleModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />

    <EditAbpRole
      @register="registerEditAbpRoleModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { BasicTable, useTable, TableAction } from "/@/components/Table";
import { tableColumns, searchFormSchema, getTableListAsync, deleteRoleAsync } from "/@/views/admin/roles/AbpRole";
import { useModal } from "/@/components/Modal";
import CreateAbpRole from "./CreateAbpRole.vue";
import PermissionAbpRole from "./PermissionAbpRole.vue";
import EditAbpRole from "./EditAbpRole.vue";
import { useMessage } from "/@/hooks/web/useMessage";
import { useDrawer } from "/@/components/Drawer";
import { useI18n } from "/@/hooks/web/useI18n";
import { Tag } from "ant-design-vue";

export default defineComponent({
  name: "AbpRole",
  components: {
    BasicTable,
    TableAction,
    CreateAbpRole,
    PermissionAbpRole,
    EditAbpRole,
    Tag
  },
  setup() {
    const { createConfirm } = useMessage();
    const { t } = useI18n();
    const [registerPermissionAbpRoleModal, { openDrawer: openPermissionAbpRoleDrawer }] =
      useDrawer();

    const [registerCreateAbpRoleModal, { openModal: openCreateAbpRoleModal }] = useModal();

    const [registerEditAbpRoleModal, { openModal: openEditAbpRoleModal }] = useModal();

    // table配置
    const [registerTable, { reload }] = useTable({
      columns: tableColumns,
      formConfig: {
        labelWidth: 70,
        schemas: searchFormSchema
      },
      api: getTableListAsync,
      showTableSetting: true,
      useSearchForm: true,
      bordered: true,
      canResize: true,
      showIndexColumn: true,
      actionColumn: {
        width: 200,
        title: t("common.action"),
        dataIndex: "action",
        slots: {
          customRender: "action"
        },
        fixed: "right"
      }
    });

    // 角色编辑
    const handleEdit = (record: Recordable) => {
      openEditAbpRoleModal(true, {
        record: record
      });
    };

    // 角色授权
    const handlePermission = (record: Recordable) => {
      openPermissionAbpRoleDrawer(true, {
        record: record
      });
    };

    // 删除角色
    const handleDelete = async (record: Recordable) => {
      let msg = t("common.askDelete");
      createConfirm({
        iconType: "warning",
        title: t("common.tip"),
        content: msg,
        onOk: async () => {
          await deleteRoleAsync({ roleId: record.id, reload });
        }
      });
    };

    return {
      t,
      registerTable,
      handleEdit,
      handleDelete,
      handlePermission,
      getTableListAsync,
      registerCreateAbpRoleModal,
      openCreateAbpRoleModal,
      registerPermissionAbpRoleModal,
      registerEditAbpRoleModal,
      reload
    };
  }
});
</script>

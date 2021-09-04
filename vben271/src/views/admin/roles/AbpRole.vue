<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #category="{ record }">
        <Tag :color="record.isDefault ? 'red' : 'green'">
          {{ record.isDefault ? '是' : '否' }}
        </Tag>
      </template>

      <template #toolbar>
        <a-button
          type="primary"
          @click="openCreateAbpRoleModal"
          v-auth="'AbpIdentity.Roles.Create'"
        >
          {{ t('common.createText') }}
        </a-button>
      </template>

      <template #action="{ record }">
        <a-button
          type="link"
          size="small"
          @click="handlePermission(record)"
          v-auth="'AbpIdentity.Roles.ManagePermissions'"
        >
          {{ t('routes.admin.roleManagement_permission') }}
        </a-button>

        <a-button
          type="link"
          size="small"
          @click="handleEdit(record)"
          v-auth="'AbpIdentity.Roles.Update'"
        >
          {{ t('common.editText') }}
        </a-button>

        <a-button
          type="link"
          size="small"
          @click="handleDelete(record)"
          v-auth="'AbpIdentity.Roles.Delete'"
        >
          {{ t('common.delText') }}
        </a-button>
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
  import { defineComponent } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { tableColumns, searchFormSchema, getTableListAsync, deleleRoleAsync } from './AbpRole';
  import { useModal } from '/@/components/Modal';
  import CreateAbpRole from './CreateAbpRole.vue';
  import PermissionAbpRole from './PermissionAbpRole.vue';
  import EditAbpRole from './EditAbpRole.vue';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { useDrawer } from '/@/components/Drawer';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { Tag } from 'ant-design-vue';
  export default defineComponent({
    name: 'AbpUser',
    components: {
      BasicTable,
      TableAction,
      CreateAbpRole,
      PermissionAbpRole,
      EditAbpRole,
      Tag,
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
          schemas: searchFormSchema,
        },
        api: getTableListAsync,
        showTableSetting: true,
        useSearchForm: true,
        bordered: true,
        canResize: true,
        showIndexColumn: true,
        actionColumn: {
          width: 200,
          title: t('common.action'),
          dataIndex: 'action',
          slots: {
            customRender: 'action',
          },
          fixed: 'right',
        },
      });

      // 角色编辑
      const handleEdit = (record: Recordable) => {
        openEditAbpRoleModal(true, {
          record: record,
        });
      };

      // 角色授权
      const handlePermission = (record: Recordable) => {
        openPermissionAbpRoleDrawer(true, {
          record: record,
        });
      };

      // 删除角色
      const handleDelete = async (record: Recordable) => {
        let msg = t('common.askDelete');
        createConfirm({
          iconType: 'warning',
          title: t('common.tip'),
          content: msg,
          onOk: async () => {
            await deleleRoleAsync({ roleId: record.id, reload });
          },
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
        reload,
      };
    },
  });
</script>

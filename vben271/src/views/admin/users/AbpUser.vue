<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button
          type="primary"
          @click="openCreateAbpUserModal"
          v-auth="'AbpIdentity.Users.Create'"
        >
          {{ t('common.createText') }}
        </a-button>
      </template>
      <template #isActive="{ record }">
        <Tag :color="record.isActive ? 'green' : 'red'">
          {{ record.isActive ? t('common.enabled') : t('common.disEnabled') }}
        </Tag>
      </template>
      <template #action="{ record }">
        <a-button
          type="link"
          size="small"
          @click="handleEdit(record)"
          v-auth="'AbpIdentity.Users.Update'"
        >
          {{ t('common.editText') }}
        </a-button>

        <a-button
          type="link"
          size="small"
          @click="handleDelete(record)"
          v-auth="'AbpIdentity.Users.Delete'"
        >
          {{ t('common.delText') }}
        </a-button>
        <a-button
          type="link"
          size="small"
          @click="handleLock(record)"
          v-auth="'System.Users.Enable'"
        >
          {{ !record.isActive ? t('common.enabled') : t('common.disEnabled') }}
        </a-button>
      </template>
    </BasicTable>
    <CreateAbpUser
      @register="registerCreateAbpUserModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
    <EditAbpUser
      @register="registerEditAbpUserModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
    <Warehouse
      @register="registerWarehosueModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
  </div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import {
    tableColumns,
    searchFormSchema,
    getTableListAsync,
    deleteUserAsync,
    lockUserAsync,
  } from './AbpUser';
  import { useModal } from '/@/components/Modal';
  import CreateAbpUser from './CreateAbpUser.vue';
  import EditAbpUser from './EditAbpUser.vue';
  import { useUserStoreWithOut } from '/@/store/modules/user';
  import { message } from 'ant-design-vue';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { LockUserInput } from '/@/services/ServiceProxies';
  import { Tag } from 'ant-design-vue';
  export default defineComponent({
    name: 'AbpUser',
    components: {
      BasicTable,
      TableAction,
      CreateAbpUser,
      EditAbpUser,
      Tag,
    },
    setup() {
      const userStore = useUserStoreWithOut();
      const userInfo = userStore.getUserInfo;
      let currentUserId = userInfo.userId;
      const { createConfirm } = useMessage();
      const { t } = useI18n();
      const [registerCreateAbpUserModal, { openModal: openCreateAbpUserModal }] = useModal();

      const [registerEditAbpUserModal, { openModal: openEditAbpUserModal }] = useModal();

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
          width: 250,
          title: t('common.action'),
          dataIndex: 'action',
          slots: {
            customRender: 'action',
          },
          fixed: 'right',
        },
      });

      // 编辑用户
      const handleEdit = (record: Recordable) => {
        openEditAbpUserModal(true, {
          record: record,
        });
      };

      // 删除用户
      const handleDelete = async (record: Recordable) => {
        if (record.name == 'admin') {
          message.error('admin not delete');
          return;
        } else {
          let msg = t('common.askDelete');
          createConfirm({
            iconType: 'warning',
            title: t('common.tip'),
            content: msg,
            onOk: async () => {
              await deleteUserAsync({ userId: record.id, reload });
            },
          });
        }
      };

      const handleLock = async (record: Recordable) => {
        if (!record.lockoutEnabled && currentUserId === record.id) {
          createConfirm({
            iconType: 'warning',
            title: t('common.tip'),
            content: t('common.disEnabledSelf'),
          });
          return;
        }
        let request = new LockUserInput();
        request.userId = record.id;
        request.locked = !record.isActive;
        await lockUserAsync(request);
        message.success(t('common.operationSuccess'));
        reload();
      };
      return {
        registerTable,
        handleEdit,
        handleDelete,
        getTableListAsync,
        registerCreateAbpUserModal,
        openCreateAbpUserModal,
        registerEditAbpUserModal,
        t,
        reload,
        handleLock,
      };
    },
  });
</script>

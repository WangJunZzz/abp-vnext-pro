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
      <template #lockoutEnabled="{ record }">
        <Tag :color="!record.lockoutEnabled ? 'green' : 'red'">
          {{ !record.lockoutEnabled  ? t('common.enable') : t('common.disable')  }}
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
          v-auth="'AbpIdentity.Users.Lock'"
        >
          {{ record.lockoutEnabled ? t('common.enable') : t('common.disable') }}
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
  </div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import {
    tableColumns,
    searchFormSchema,
    getTableListAsync,
    deleteUserAsync,
    lockUserAsync,
  } from './AbpUser';
  import { LockUserInput } from '/@/services/ServiceProxies';
  import { useModal } from '/@/components/Modal';
  import CreateAbpUser from './CreateAbpUser.vue';
  import EditAbpUser from './EditAbpUser.vue';
  import { message } from 'ant-design-vue';
  import { useI18n } from '/@/hooks/web/useI18n';
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
      const { t } = useI18n();
      const [registerCreateAbpUserModal, { openModal: openCreateAbpUserModal }] = useModal();

      const [registerEditAbpUserModal, { openModal: openEditAbpUserModal }] = useModal();

      // table配置
      const [registerTable, { reload }] = useTable({
        columns: tableColumns,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
        },
        api: getTableListAsync,
        showTableSetting: true,
        useSearchForm: true,
        bordered: true,
        canResize: false,
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
        }
        await deleteUserAsync({ userId: record.id, reload });
      };

      const handleLock = async (record: Recordable) => {
        let request = new LockUserInput();
        request.userId = record.id;
        request.locked = !record.lockoutEnabled;
        await lockUserAsync(request);
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

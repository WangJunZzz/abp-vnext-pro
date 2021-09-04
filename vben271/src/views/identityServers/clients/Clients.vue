<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button
          type="primary"
          @click="openCreateClientModal"
          v-auth="'IdentityServerManagement.Client.Create'"
        >
          {{ t('common.createText') }}
        </a-button>
      </template>

      <template #enabled="{ record }">
        <Tag :color="record.enabled ? 'green' : 'red'">
          {{ record.enabled ? t('common.true') : t('common.false') }}
        </Tag>
      </template>

      <template #action="{ record }">
        <a-button
          type="link"
          size="small"
          @click="handleEdit(record)"
          v-auth="'IdentityServerManagement.Client.Update'"
        >
          {{ t('common.editText') }}
        </a-button>
        <a-button
          type="link"
          size="small"
          @click="handleEnabled(record)"
          v-auth="'IdentityServerManagement.Client.Enable'"
        >
          {{ record.enabled ? t('common.enabled') : t('common.disEnabled') }}
        </a-button>
        <a-button
          type="link"
          size="small"
          @click="handleUri(record)"
          v-auth="'IdentityServerManagement.Client.Update'"
        >
          Uri
        </a-button>
        <a-button
          type="link"
          size="small"
          @click="handleDelete(record)"
          v-auth="'IdentityServerManagement.Client.Delete'"
        >
          {{ t('common.delText') }}
        </a-button>
      </template>
    </BasicTable>
    <CreateClient
      @register="registerCreateClientModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
    <EditClientBasic
      @register="registerEditClientModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
    <ClientUri @register="registerUriDrawer" @reload="reload" :bodyStyle="{ 'padding-top': '0' }" />
  </div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import {
    tableColumns,
    searchFormSchema,
    getTableListAsync,
    deleteClientAsync,
    enabledClientAsync,
  } from './Clients';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { Tag } from 'ant-design-vue';
  import CreateClient from './CreateClient.vue';
  import EditClientBasic from './EditClientBasic.vue';
  import ClientUri from './ClientUri.vue';
  import { useModal } from '/@/components/Modal';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { useDrawer } from '/@/components/Drawer';

  export default defineComponent({
    name: 'Clients',
    components: {
      BasicTable,
      TableAction,
      Tag,
      CreateClient,
      EditClientBasic,
      ClientUri,
    },
    setup() {
      const { t } = useI18n();
      const { createConfirm } = useMessage();
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
          width: 300,
          title: t('common.action'),
          dataIndex: 'action',
          slots: {
            customRender: 'action',
          },
          fixed: 'right',
        },
      });

      const [registerCreateClientModal, { openModal: openCreateClientModal }] = useModal();
      const [registerEditClientModal, { openModal: openEditClientModal }] = useModal();
      const handleEdit = (record: Recordable) => {
        openEditClientModal(true, {
          record: record,
        });
      };
      const handleDelete = (record: Recordable) => {
        let msg = t('common.askDelete');
        createConfirm({
          iconType: 'warning',
          title: t('common.tip'),
          content: msg,
          onOk: async () => {
            await deleteClientAsync({ id: record.id, reload });
          },
        });
      };
      const handleEnabled = async (record: Recordable) => {
        await enabledClientAsync({ clientId: record.clientId, enabled: !record.enabled, reload });
      };
      const [registerUriDrawer, { openDrawer: openUriDrawer }] = useDrawer();
      const handleUri = async (record: Recordable) => {
        openUriDrawer(true, { record: record });
      };
      return {
        registerTable,
        registerCreateClientModal,
        openCreateClientModal,
        registerEditClientModal,
        openEditClientModal,
        t,
        reload,
        handleEdit,
        handleDelete,
        handleEnabled,
        handleUri,
        registerUriDrawer,
      };
    },
  });
</script>

<style lang="less" scoped></style>

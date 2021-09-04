<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button
          type="primary"
          @click="openCreateApiResourceModal"
          v-auth="'IdentityServerManagement.ApiResource.Create'"
        >
          {{ t('common.createText') }}
        </a-button>
      </template>
      <template #enabled="{ record }">
        <Tag :color="record.enabled ? 'green' : 'red'">
          {{ record.enabled ? t('common.enabled') : t('common.disEnabled') }}
        </Tag>
      </template>
      <template #showInDiscoveryDocument="{ record }">
        <Tag :color="record.showInDiscoveryDocument ? 'green' : 'red'">
          {{ record.showInDiscoveryDocument ? t('common.enabled') : t('common.disEnabled') }}
        </Tag>
      </template>
      <template #action="{ record }">
        <a-button
          type="link"
          size="small"
          @click="handleEdit(record)"
          v-auth="'IdentityServerManagement.ApiResource.Update'"
        >
          {{ t('common.editText') }}
        </a-button>

        <a-button
          type="link"
          size="small"
          @click="handleDelete(record)"
          v-auth="'IdentityServerManagement.ApiResource.Delete'"
        >
          {{ t('common.delText') }}
        </a-button>
      </template>
    </BasicTable>
    <CreateApiResource
      @register="registerCreateApiResourceModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }" />
    <EditApiResources
      @register="registerEditApiResourceModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
  /></div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import {
    tableColumns,
    searchFormSchema,
    getTableListAsync,
    deleteApiResourceAsync,
  } from './ApiResources';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { Tag } from 'ant-design-vue';
  import CreateApiResource from './CreateApiResource.vue';
  import EditApiResources from './EditApiResources.vue';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { useModal } from '/@/components/Modal';
  export default defineComponent({
    name: 'ApiResources',
    components: {
      BasicTable,
      TableAction,
      Tag,
      CreateApiResource,
      EditApiResources,
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
          width: 150,
          title: t('common.action'),
          dataIndex: 'action',
          slots: {
            customRender: 'action',
          },
          fixed: 'right',
        },
      });
      const [registerCreateApiResourceModal, { openModal: openCreateApiResourceModal }] =
        useModal();
      const [registerEditApiResourceModal, { openModal: openEditApiResourceModal }] = useModal();
      const handleDelete = async (record: Recordable) => {
        let msg = t('common.askDelete');
        createConfirm({
          iconType: 'warning',
          title: t('common.tip'),
          content: msg,
          onOk: async () => {
            await deleteApiResourceAsync({ id: record.id, reload });
          },
        });
      };
      const handleEdit = (record: Recordable) => {
        openEditApiResourceModal(true, {
          record: record,
        });
      };
      return {
        reload,
        t,
        registerTable,
        handleDelete,
        handleEdit,
        registerCreateApiResourceModal,
        openCreateApiResourceModal,
        registerEditApiResourceModal,
      };
    },
  });
</script>

<style lang="less" scoped></style>

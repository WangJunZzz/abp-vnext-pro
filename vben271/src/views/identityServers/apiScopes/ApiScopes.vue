<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button type="primary" @click="openCreateApiScopeModal">
          {{ t('common.createText') }}
        </a-button>
      </template>
      <template #enabled="{ record }">
        <Tag :color="record.enabled ? 'green' : 'red'">
          {{ record.enabled ? '是' : '否' }}
        </Tag>
      </template>
      <template #required="{ record }">
        <Tag :color="record.required ? 'green' : 'red'">
          {{ record.required ? '是' : '否' }}
        </Tag>
      </template>
      <template #emphasize="{ record }">
        <Tag :color="record.emphasize ? 'green' : 'red'">
          {{ record.emphasize ? '是' : '否' }}
        </Tag>
      </template>
      <template #showInDiscoveryDocument="{ record }">
        <Tag :color="record.showInDiscoveryDocument ? 'green' : 'red'">
          {{ record.showInDiscoveryDocument ? '是' : '否' }}
        </Tag>
      </template>
      <template #action="{ record }">
        <a-button type="link" size="small" @click="handleEdit(record)">
          {{ t('common.editText') }}
        </a-button>

        <a-button type="link" size="small" @click="handleDelete(record)">
          {{ t('common.delText') }}
        </a-button>
      </template>
    </BasicTable>
    <CreateApiScope @register="registerCreateApiScopeModal" @reload="reload" :bodyStyle="{ 'padding-top': '0' }" />
    <EditApiScope @register="registerEditApiScopeModal" @reload="reload" :bodyStyle="{ 'padding-top': '0' }"
  /></div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { tableColumns, searchFormSchema, getTableListAsync, deleteApiScopeAsync } from './ApiScopes';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { Tag } from 'ant-design-vue';
  import CreateApiScope from './CreateApiScope.vue';
  import EditApiScope from './EditApiScope.vue';
  import { useModal } from '/@/components/Modal';
  import { useMessage } from '/@/hooks/web/useMessage';
  export default defineComponent({
    name: 'ApiScopes',
    components: {
      BasicTable,
      TableAction,
      Tag,
      CreateApiScope,
      EditApiScope,
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
      const [registerCreateApiScopeModal, { openModal: openCreateApiScopeModal }] = useModal();
      const [registerEditApiScopeModal, { openModal: openEditApiScopeModal }] = useModal();
      const handleEdit = (record: Recordable) => {
        openEditApiScopeModal(true, { record: record });
      };
      const handleDelete = (record: Recordable) => {
        let msg = '是否确认删除';
        createConfirm({
          iconType: 'warning',
          title: '提示',
          content: msg,
          onOk: async () => {
            await deleteApiScopeAsync({ id: record.id, reload });
          },
        });
      };
      return {
        t,
        registerTable,
        openCreateApiScopeModal,
        registerCreateApiScopeModal,
        registerEditApiScopeModal,
        openEditApiScopeModal,
        reload,
        handleDelete,
        handleEdit,
      };
    },
  });
</script>

<style lang="less" scoped></style>

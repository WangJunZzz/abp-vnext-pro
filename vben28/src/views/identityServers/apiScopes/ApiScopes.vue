<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button
          preIcon="ant-design:plus-circle-outlined"
          type="primary"
          @click="openCreateApiScopeModal"
          v-auth="'IdentityServerManagement.ApiScope.Create'"
        >
          {{ t('common.createText') }}
        </a-button>
      </template>

    <template #bodyCell="{ column, record }">
      <template v-if="column.key === 'enabled'">
        <Tag :color="record.enabled ? 'green' : 'red'">
          {{ record.enabled ? t('common.true') : t('common.false') }}
        </Tag>
      </template>
      <template v-if="column.key === 'required'">
        <Tag :color="record.required ? 'green' : 'red'">
          {{ record.required ? t('common.true') : t('common.false') }}
        </Tag>
      </template>
      <template v-if="column.key === 'emphasize'">
        <Tag :color="record.emphasize ? 'green' : 'red'">
          {{ record.emphasize ? t('common.true') : t('common.false') }}
        </Tag>
      </template>
      <template v-if="column.key === 'showInDiscoveryDocument'">
        <Tag :color="record.showInDiscoveryDocument ? 'green' : 'red'">
          {{ record.showInDiscoveryDocument ? t('common.true') : t('common.false') }}
        </Tag>
      </template>      
    </template>

      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              auth: 'IdentityServerManagement.ApiScope.Update',
              label: t('common.editText'),
              icon: 'ant-design:edit-outlined',
              onClick: handleEdit.bind(null, record),
            },
            {
              icon: 'ic:outline-delete-outline',
              auth: 'IdentityServerManagement.ApiScope.Delete',
              label: t('common.delText'),
              onClick: handleDelete.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>
    <CreateApiScope
      @register="registerCreateApiScopeModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }" />
    <EditApiScope
      @register="registerEditApiScopeModal"
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
    deleteApiScopeAsync,
  } from "/@/views/identityServers/apiScopes/ApiScopes";
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
          width: 120,
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
        let msg = t('common.askDelete');
        createConfirm({
          iconType: 'warning',
          title: t('common.tip'),
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

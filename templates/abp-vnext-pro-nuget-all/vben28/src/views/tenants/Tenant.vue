<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button
          preIcon="ant-design:plus-circle-outlined"
          type="primary"
          @click="openCreateTenantModal"
          v-auth="'AbpTenantManagement.Tenants.Create'"
        >
          {{ t('common.createText') }}
        </a-button>
      </template>

      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'ant-design:edit-outlined',
              auth: 'AbpTenantManagement.Tenants.Update',
              label: t('common.editText'),
              onClick: handleEdit.bind(null, record),
            },
            {
              icon: 'ant-design:edit-outlined',
              auth: 'AbpTenantManagement.Tenants.ManageConnectionStrings',
              label: t('routes.tenant.connectionString'),
              onClick: handleConnectionString.bind(null, record),
            },
            {
              icon: 'ant-design:edit-outlined',
              auth: 'AbpTenantManagement.Tenants.ManageFeatures',
              label: t('routes.tenant.manageFeatures'),
              onClick: handleManageFeatures.bind(null, record),
            },
            {
              icon: 'ant-design:minus-outlined',
              auth: 'AbpTenantManagement.Tenants.Delete',
              label: t('common.delText'),
              onClick: handleDelete.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>
    <EditTenant
      @register="registerEditTenantModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
    <CreateTenant
      @register="registerCreateTenantModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
    <EditConnectionString
      @register="registerEditConnectionStringModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
    <ManageFeatrue
      @register="registerTenantFeatureModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
  </div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { useModal } from '/@/components/Modal';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';

  import {
    getTenantListAsync,
    tableColumns,
    searchFormSchema,
    deleteTenantAsync,
  } from '/@/views/tenants/Tenant';
  import CreateTenant from './CreateTenant.vue';
  import ManageFeatrue from './ManageFeatrue.vue';
  import EditConnectionString from './EditConnectionString.vue';
  import EditTenant from './EditTenant.vue';
  import { useMessage } from '/@/hooks/web/useMessage';
  export default defineComponent({
    name: 'Tenant',
    components: {
      BasicTable,
      TableAction,
      CreateTenant,
      EditTenant,
      EditConnectionString,
      ManageFeatrue
    },
    setup() {
      const { t } = useI18n();

      const [registerCreateTenantModal, { openModal: openCreateTenantModal }] = useModal();
      const [registerTenantFeatureModal, { openModal: openFeatureTenantModal }] = useModal();
      const [registerEditTenantModal, { openModal: openEditTenantModal }] = useModal();
      const [registerEditConnectionStringModal, { openModal: openEditConnectionStringModal }] =
        useModal();
      const [registerTable, { reload }] = useTable({
        columns: tableColumns,
        formConfig: {
          labelWidth: 100,
          schemas: searchFormSchema,
        },
        api: getTenantListAsync,
        useSearchForm: true,
        showTableSetting: true,
        bordered: true,
        canResize: true,
        showIndexColumn: true,
        actionColumn: {
          title: t('common.action'),
          dataIndex: 'action',
          slots: {
            customRender: 'action',
          },
          width: 350,
          fixed: 'right',
        },
      });
      const { createConfirm } = useMessage();
      // 编辑
      const handleEdit = (record: Recordable) => {
        openEditTenantModal(true, { record: record });
      };
    
      // 删除
      const handleDelete = async (record: Recordable) => {
        let msg = t('common.askDelete');
        createConfirm({
          iconType: 'warning',
          title: t('common.tip'),
          content: msg,
          onOk: async () => {
            await deleteTenantAsync({ id: record.id });
            await reload();
          },
        });
      };

      const handleConnectionString = async (record: Recordable) => {
        openEditConnectionStringModal(true, { record: record });
      };
      const handleManageFeatures = async (record: Recordable) => {
        openFeatureTenantModal(true, { id: record.id });
      };
      return {
        t,
        registerTable,
        reload,
        openEditTenantModal,
        openCreateTenantModal,
        registerCreateTenantModal,
        handleConnectionString,
        handleDelete,
        handleEdit,
        registerEditTenantModal,
        registerEditConnectionStringModal,
        handleManageFeatures,
        registerTenantFeatureModal
      };
    },
  });
</script>

<style lang="less" scoped></style>

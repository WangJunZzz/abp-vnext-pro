<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button type="primary" @click="openCreateIdentityResourcesModal">
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
    <CreateIdentityResource @register="registerCreatIdentityResourcesModal" @reload="reload" :bodyStyle="{ 'padding-top': '0' }" />
    <EditIdentityResources @register="registerEditIdentityResourcesModal" @reload="reload" :bodyStyle="{ 'padding-top': '0' }"
  /></div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { tableColumns, searchFormSchema, getTableListAsync, deleteIdentityResourcesAsync } from './IdentityResources';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { Tag } from 'ant-design-vue';
  import CreateIdentityResource from './CreateIdentityResource.vue';
  import EditIdentityResources from './EditIdentityResources.vue';
  import { useModal } from '/@/components/Modal';
  import { useMessage } from '/@/hooks/web/useMessage';
  export default defineComponent({
    name: 'IdentityResources',
    components: {
      BasicTable,
      TableAction,
      Tag,
      CreateIdentityResource,
      EditIdentityResources,
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
      const [registerCreatIdentityResourcesModal, { openModal: openCreateIdentityResourcesModal }] = useModal();
      const [registerEditIdentityResourcesModal, { openModal: openEditIdentityResourcesModal }] = useModal();
      const handleEdit = (record: Recordable) => {
        openEditIdentityResourcesModal(true, { record: record });
      };
      const handleDelete = (record: Recordable) => {
        let msg = '是否确认删除';
        createConfirm({
          iconType: 'warning',
          title: '提示',
          content: msg,
          onOk: async () => {
            await deleteIdentityResourcesAsync({ id: record.id, reload });
          },
        });
      };
      return {
        t,
        registerTable,
        registerCreatIdentityResourcesModal,
        registerEditIdentityResourcesModal,
        openCreateIdentityResourcesModal,
        openEditIdentityResourcesModal,
        handleDelete,
        handleEdit,
        reload,
      };
    },
  });
</script>

<style lang="less" scoped></style>

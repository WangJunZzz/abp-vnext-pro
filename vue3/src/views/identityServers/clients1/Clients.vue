<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button type="primary" @click="openCreateClientModal">
          {{ t('common.createText') }}
        </a-button>
      </template>
      <template #enabled="{ record }">
        <Tag :color="record.enabled ? 'green' : 'red'">
          {{ record.enabled ? '是' : '否' }}
        </Tag>
      </template>
    </BasicTable>
    <CreateClient @register="registerCreateClientModal" @reload="reload" :bodyStyle="{ 'padding-top': '0' }" />
  </div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { tableColumns, searchFormSchema, getTableListAsync } from './Clients';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { Tag } from 'ant-design-vue';
  import CreateClient from './CreateClient.vue';
  import { useModal } from '/@/components/Modal';
  export default defineComponent({
    name: 'Clients',
    components: {
      BasicTable,
      TableAction,
      Tag,
      CreateClient,
    },
    setup() {
      const { t } = useI18n();
      // table配置
      const [registerTable] = useTable({
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

      const [registerCreateClientModal, { openModal: openCreateClientModal }] = useModal();
      return {
        registerTable,
        registerCreateClientModal,
        openCreateClientModal,
      };
    },
  });
</script>

<style lang="less" scoped></style>

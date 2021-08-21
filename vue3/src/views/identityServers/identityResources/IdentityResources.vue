<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #enabled="{ record }">
        <Tag :color="record.enabled ? 'green' : 'red'">
          {{ record.enabled ? '是' : '否' }}
        </Tag>
      </template>
    </BasicTable></div
  >
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { tableColumns, searchFormSchema, getTableListAsync } from './IdentityResources';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { Tag } from 'ant-design-vue';
  export default defineComponent({
    name: 'IdentityResources',
    components: {
      BasicTable,
      TableAction,
      Tag,
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

      return {
        registerTable,
      };
    },
  });
</script>

<style lang="less" scoped></style>

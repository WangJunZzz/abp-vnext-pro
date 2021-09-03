<template>
  <div>
    <BasicTable @register="registerTable" size="small"> </BasicTable>
  </div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { tableColumns, searchFormSchema, getTableListAsync } from './AuditLog';
  import { Tag } from 'ant-design-vue';
  import { useI18n } from '/@/hooks/web/useI18n';
  export default defineComponent({
    name: 'AuditLog',
    components: {
      BasicTable,
      TableAction,
      Tag,
    },
    setup() {
      const { t } = useI18n();
      // table配置
      const [registerTable, { reload }] = useTable({
        columns: tableColumns,
        formConfig: {
          labelWidth: 70,
          schemas: searchFormSchema,
          fieldMapToTime: [
            ['time', ['executionBeginTime', 'executionEndTime'], 'YYYY-MM-DD HH:mm:ss'],
          ],
        },
        api: getTableListAsync,
        showTableSetting: true,
        useSearchForm: true,
        bordered: true,
        canResize: true,
        showIndexColumn: true,
      });

      return {
        registerTable,
        reload,
        t,
      };
    },
  });
</script>

<template>
  <div>
    <BasicTable @register="registerTable" size="small">
    </BasicTable>
  </div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { useI18n } from '/src/hooks/web/useI18n';
  import { BasicTable, TableAction, useTable } from '/src/components/Table';
  import { tableColumns, searchFormSchema, pageAsync} from './Index';
  export default defineComponent({
    name: 'IdentitySecurityLog',
    components: {
      BasicTable,
      TableAction,
    },
    setup() {
      const { t } = useI18n();
      // table配置
      const [registerTable, { reload }] = useTable({
        columns: tableColumns,
        formConfig: {
          labelWidth: 70,
          schemas: searchFormSchema,
          fieldMapToTime: [['time', ['startTime', 'endTime'], 'YYYY-MM-DD']],
        },
        api: pageAsync,
        showTableSetting: true,
        useSearchForm: true,
        bordered: true,
        canResize: true,
        showIndexColumn: true,
        immediate: true,
        scroll: { x: true },
      });

      return {
        registerTable,
        reload,
        t,
      };
    },
  });
</script>

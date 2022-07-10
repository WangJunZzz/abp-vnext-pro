<template>
  <div>
    <BasicTable @register="registerTable" size="small">
    <template #bodyCell="{ column, record }">
      <template v-if="column.key === 'url'">
        <Tag :color="httpStatusCodeColor(record.httpStatusCode)">{{ record.httpStatusCode }}</Tag>
        <Tag style="margin-left: 5px" :color="httpMethodColor(record.httpMethod)">
          {{ record.httpMethod }}
        </Tag>
        <span style="margin-left: 5px">{{ record.url }}</span>
      </template>
    </template>
    </BasicTable>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { BasicTable, useTable } from "/@/components/Table";
import { tableColumns, searchFormSchema, getTableListAsync, httpStatusCodeColor, httpMethodColor } from "/@/views/admin/auditLog/AuditLog";
import { Tag } from "ant-design-vue";
import { useI18n } from "/@/hooks/web/useI18n";

export default defineComponent({
  name: "AuditLog",
  components: {
    BasicTable,
    Tag
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
          ["time", ["executionBeginTime", "executionEndTime"], "YYYY-MM-DD HH:mm:ss"]
        ]
      },
      api: getTableListAsync,
      showTableSetting: true,
      useSearchForm: true,
      bordered: true,
      canResize: true,
      showIndexColumn: true,
      immediate: true,
      scroll: { x: true }
    });

    return {
      registerTable,
      reload,
      t,
      httpStatusCodeColor,
      httpMethodColor
    };
  }
});
</script>

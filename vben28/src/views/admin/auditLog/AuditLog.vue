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
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              label: t('routes.admin.detail'),
              icon: 'ant-design:schedule-outlined',
              onClick: handleDetail.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>
    <AuditLogDetail @register="registerDrawer" />
  </div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import {
    tableColumns,
    searchFormSchema,
    getTableListAsync,
    httpStatusCodeColor,
    httpMethodColor,
  } from '/@/views/admin/auditLog/AuditLog';
  import AuditLogDetail from './AuditLogDetail.vue';
  import { Tag } from 'ant-design-vue';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { useDrawer } from '/@/components/Drawer';
  export default defineComponent({
    name: 'AuditLog',
    components: {
      BasicTable,
      Tag,
      AuditLogDetail,
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
        api: getTableListAsync,
        showTableSetting: true,
        useSearchForm: true,
        bordered: true,
        canResize: true,
        showIndexColumn: true,
        immediate: true,
        scroll: { x: true },
        actionColumn: {
          width: 200,
          title: t('common.action'),
          dataIndex: 'action',
          slots: {
            customRender: 'action',
          },
          fixed: 'right',
        },
      });
      const [registerDrawer, { openDrawer }] = useDrawer();
      const handleDetail = (record: Recordable) => {
        openDrawer(true, {
          record: record,
        });
      };
      return {
        registerTable,
        reload,
        t,
        httpStatusCodeColor,
        httpMethodColor,
        registerDrawer,
        handleDetail,
      };
    },
  });
</script>

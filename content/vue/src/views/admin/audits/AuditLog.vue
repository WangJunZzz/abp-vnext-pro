<template>
  <div>
    <BasicTable @register="registerAuditTable" size="small">
      <template #category="{ record }">
        <Tag :color="record.httpStatusCode >= 500 ? 'red' : 'green'">
          {{ record.httpStatusCode }}
        </Tag>

        <Tag :color="record.httpMethod == 'DELETE' ? 'red' : 'green'">
          {{ record.httpMethod }}
        </Tag>
      </template>
      <template #action="{ record }">
        <a-button type="link" size="small" @click="lookJson(record.id)">
          {{ t('routes.admin.audit_entityInfo') }}
        </a-button>
      </template>
    </BasicTable>
    <BasicModal
      :title="t('routes.admin.audit_message')"
      :canFullscreen="false"
      @register="registerModal"
      :showOkButton="false"
    >
      <json-viewer :value="jsonRef" copyable boxed sort />
    </BasicModal>
  </div>
</template>

<script lang="ts">
  import { Tag } from 'ant-design-vue';
  import { BasicTable, useTable } from '/@/components/Table';
  import { BasicModal, useModal } from '/@/components/Modal';
  import { defineComponent, ref } from 'vue';
  import { tableColumns, getTableListAsync, getEntityInfoAsync } from './audit';
  import { searchFormSchema } from './audit';
  import { useI18n } from '/@/hooks/web/useI18n';

  export default defineComponent({
    name: 'AuditLog',
    components: {
      BasicTable,
      Tag,
      BasicModal,
    },
    setup() {
      const { t } = useI18n();
      const [registerModal, { openModal: openJsonModal }] = useModal();
      const [registerAuditTable, { reload }] = useTable({
        columns: tableColumns,
        formConfig: {
          labelWidth: 80,
          schemas: searchFormSchema,
        },
        api: getTableListAsync,
        useSearchForm: true,
        showTableSetting: true,
        bordered: true,
        canResize: false,
        showIndexColumn: true,
        actionColumn: {
          title: t('common.action'),
          dataIndex: 'action',
          slots: {
            customRender: 'action',
          },
          width: 150,
          fixed: 'right',
        },
      });
      const jsonRef = ref('');
      const lookJson = async (id: string) => {
        openJsonModal();
        let result = await getEntityInfoAsync(id);
        jsonRef.value = result as any;
      };

      return {
        t,
        registerAuditTable,
        reload,
        getTableListAsync,
        lookJson,
        registerModal,
        openJsonModal,
        jsonRef,
      };
    },
  });
</script>
<style lang="less" scoped></style>

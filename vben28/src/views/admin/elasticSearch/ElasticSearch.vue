<template>
  <div>
    <BasicTable @register="registerTable" size="small">

    <template #bodyCell="{ column, record }">
      <template v-if="column.key === 'message'">
         <a-button type="link" size="small" @click="lookJson(record)">
          {{ t("common.detail") }}
        </a-button>
      </template>
    </template>
    </BasicTable>
    <BasicModal :canFullscreen="false" @register="registerModal" :showOkButton="false">
      {{ content }}
    </BasicModal>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { useI18n } from "/@/hooks/web/useI18n";
import { BasicModal, useModal } from "/@/components/Modal";
import { BasicTable, useTable } from "/@/components/Table";
import {
  getElasticSearchLogAsync,
  tableColumns,
  searchFormSchema
} from "/@/views/admin/elasticSearch/ElasticSearch";

export default defineComponent({
  name: "ElasticSearch",
  components: {
    BasicTable,
    BasicModal
  },
  setup() {
    const { t } = useI18n();
    const [registerModal, { openModal: openJsonModal }] = useModal();
    const [registerTable, { reload }] = useTable({
      columns: tableColumns,
      formConfig: {
        labelWidth: 100,
        schemas: searchFormSchema,
        fieldMapToTime: [["time", ["startCreationTime", "endCreationTime"]]]
      },
      api: getElasticSearchLogAsync,
      useSearchForm: true,
      showTableSetting: true,
      bordered: true,
      canResize: true,
      showIndexColumn: true,
      actionColumn: {
        title: t("common.action"),
        dataIndex: "action",
        slots: {
          customRender: "action"
        },
        width: 150,
        fixed: "right"
      }
    });
    let content = ref("");
    const lookJson = async (record) => {
      openJsonModal();
      content.value = record.message;
    };

    return {
      t,
      registerTable,
      reload,
      lookJson,
      registerModal,
      openJsonModal,
      content
    };
  }
});
</script>

<style lang="less" scoped></style>

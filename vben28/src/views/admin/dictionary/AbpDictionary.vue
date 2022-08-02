<template>
  <div>
    <PageWrapper dense contentFullHeight fixedHeight contentClass="flex">
      <BasicTable
        @register="registerTypeTable"
        class="w-1/4 xl:w-1/5"
        size="small"
        @selection-change="onSelectChange"
        :clickToRowSelect="false"
      >
    <template #bodyCell="{ column, record }">
      <template v-if="column.key === 'text'">
           {{ record.code }}|
          {{ record.displayText }}
      </template>
     
    </template>

        <template #toolbar>
          <a-button
            v-auth="'AbpIdentity.DataDictionaryManagement.Create'"
            type="primary"
            preIcon="ant-design:plus-circle-outlined"
            @click="handleCreateType"
          >
            {{ t("common.createText") }}
          </a-button
          >
        </template>
        <template #action="{ record }">
          <TableAction
            :dropDownActions="[
              {
                label: t('common.editText'),
                auth:'AbpIdentity.DataDictionaryManagement.Update',
                onClick: handleEditType.bind(null, record),
              },
              {
                label: t('common.delText'),
                 auth:'AbpIdentity.DataDictionaryManagement.Delete',
                onClick: handleDeleteDictionaryType.bind(null, record),
              },
            ]"
          />
        </template>
      </BasicTable>

      <BasicTable @register="registerTable" class="w-3/4 xl:w-4/5" size="small">
        <template #toolbar>
          <a-button
            v-auth="'AbpIdentity.DataDictionaryManagement.Create'"
            preIcon="ant-design:plus-circle-outlined"
            type="primary"
            @click="handleCreate"
          >
            {{ t("common.createText") }}
          </a-button
          >
        </template>

        <template #action="{ record }">
          <TableAction
            :actions="[
              {
                icon: 'ant-design:edit-outlined',
                auth: 'AbpIdentity.DataDictionaryManagement.Update',
                label: t('common.editText'),
                onClick: handleEdit.bind(null, record),
              },
              {
                icon: 'ic:outline-delete-outline',
                auth: 'AbpIdentity.DataDictionaryManagement.Delete',
                label: t('common.delText'),
                onClick: handleDelete.bind(null, record),
              },
            ]"
          />
        </template>
      </BasicTable>
      <CreateAbpDictionaryType @register="registerCreateType" @reload="reloadType" />
      <CreateAbpDictionary @register="registerCreateModal" @reload="reload" />
      <EditAbpDictionary @register="registerEditModal" @reload="reload" />
      <EditAbpDictionaryType @register="registerEditTypeModal" @reload="reloadType" />
    </PageWrapper>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { BasicTable, useTable, TableAction } from "/@/components/Table";
import { PageWrapper } from '/@/components/Page';
import { useModal } from "/@/components/Modal";
import CreateAbpDictionary from "./CreateAbpDictionary.vue";
import EditAbpDictionary from "./EditAbpDictionary.vue";
import EditAbpDictionaryType from "./EditAbpDictionaryType.vue";
import CreateAbpDictionaryType from "./CreateAbpDictionaryType.vue";

import { useI18n } from "/@/hooks/web/useI18n";
import {
  tableColumns,
  searchFormSchema,
  getDictionaryTypeAsync,
  dictionaryTypeTableColumns,
  searchDictionaryFormSchema,
  getDictionaryDetailsAsync,
  deleteDetailAsync,
  deleteDictionaryTypeAsync
} from "/@/views/admin/dictionary/AbpDictionary";
import { useMessage } from "/@/hooks/web/useMessage";
import { Tag, message } from "ant-design-vue";

export default defineComponent({
  name: "AbpDictionary",
  components: {
    BasicTable,
    PageWrapper,
    TableAction,
    Tag,
    CreateAbpDictionaryType,
    CreateAbpDictionary,
    EditAbpDictionary,
    EditAbpDictionaryType
  },
  setup() {
    const { t } = useI18n();

    const [registerCreateModal, { openModal: createModal }] = useModal();
    const [registerEditModal, { openModal: editModal }] = useModal();
    const [registerEditTypeModal, { openModal: editTypeModal }] = useModal();
    const [registerCreateType, { openModal: createTypeModal }] = useModal();
    const selectedDataDictionaryIdRef = ref("");
    const selectedDataDictionaryDisplayTextRef = ref("");

    //左边表格
    const [registerTypeTable, { reload: reloadType, clearSelectedRowKeys }] = useTable({
      columns: dictionaryTypeTableColumns,
      formConfig: {
        labelWidth: 0,
        schemas: searchDictionaryFormSchema,
        showResetButton: false
      },
      api: getDictionaryTypeAsync,
      useSearchForm: true,
      showTableSetting: false,
      showIndexColumn: false,
      bordered: true,
      canResize: true,
      rowSelection: { type: "radio" },
      pagination: false,
      actionColumn: {
        width: 50,
        title: t("common.action"),
        dataIndex: "action",
        slots: { customRender: "action" }
      }
    });

    //勾选事件
    const onSelectChange = ({ rows }) => {
      selectedDataDictionaryIdRef.value = rows[0].id;
      selectedDataDictionaryDisplayTextRef.value = rows[0].displayText;
      reload();
    };

    const handleCreate = () => {
      if (selectedDataDictionaryIdRef.value == "") {
        message.error(t("routes.admin.chooseDictionary"));
        return;
      } else {
        let dictionaryCreate = {
          id: selectedDataDictionaryIdRef.value,
          displayText: selectedDataDictionaryDisplayTextRef.value
        };
        createModal(true, { dictionaryCreate: dictionaryCreate });
      }
    };
    const handleEditType = (record: Recordable) => {
      editTypeModal(true, {
        record: record
      });
    };
    const handleDeleteDictionaryType = async (record: Recordable) => {
      let msg = t("common.askDelete");
      createConfirm({
        iconType: "warning",
        title: t("common.tip"),
        content: msg,
        onOk: async () => {
          await deleteDictionaryTypeAsync({
            id: record.id,
            reloadType
          });
          await reload();
        }
      });
    };
    const [registerTable, { reload }] = useTable({
      columns: tableColumns,
      formConfig: {
        labelWidth: 120,
        schemas: searchFormSchema
      },
      api: getDictionaryPageDetailsAsync,
      useSearchForm: true,
      showTableSetting: true,
      showIndexColumn: true,
      bordered: true,
      canResize: true,
      actionColumn: {
        width: 150,
        title: t("common.action"),
        dataIndex: "action",
        slots: { customRender: "action" }
      }
    });

    async function getDictionaryPageDetailsAsync(params) {
      if (selectedDataDictionaryIdRef.value == "") {
        return [];
      }
      params.dataDictionaryId = selectedDataDictionaryIdRef.value;
      return await getDictionaryDetailsAsync({ params });
    }

    const handleEdit = (record: Recordable) => {
      editModal(true, {
        record: record
      });
    };
    const handleCreateType = () => {
      createTypeModal(true);
    };
    const { createConfirm } = useMessage();
    const handleDelete = async (record: Recordable) => {
      let msg = t("common.askDelete");
      createConfirm({
        iconType: "warning",
        title: t("common.tip"),
        content: msg,
        onOk: async () => {
          await deleteDetailAsync({
            dataDictionaryId: record.dataDictionaryId,
            dataDictionaryDetailId: record.id,
            reload
          });
        }
      });
    };
    return {
      registerTable,
      registerCreateModal,
      registerEditModal,
      registerEditTypeModal,
      handleCreate,
      handleEdit,
      handleEditType,
      reload,
      registerTypeTable,
      registerCreateType,
      handleCreateType,
      reloadType,
      onSelectChange,
      clearSelectedRowKeys,
      t,
      handleDelete,
      handleDeleteDictionaryType
    };
  }
});
</script>

<style lang="less" scoped></style>

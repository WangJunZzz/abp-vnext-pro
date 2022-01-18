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
        <template #toolbar>
          <a-button
            type="primary"
            preIcon="ant-design:plus-circle-outlined"
            @click="handleCreateType"
          >
            {{ t('common.createText') }}</a-button
          >
        </template>
      </BasicTable>

      <BasicTable @register="registerTable" class="w-3/4 xl:w-4/5" size="small">
        <template #toolbar>
          <a-button preIcon="ant-design:plus-circle-outlined" type="primary" @click="handleCreate">
            {{ t('common.createText') }}</a-button
          >
        </template>

        <template #action="{ record }">
          <TableAction
            :actions="[
              {
                icon: 'ant-design:edit-outlined',
                label: t('common.editText'),
                onClick: handleEdit.bind(null, record),
              },
            ]"
          />
        </template>
      </BasicTable>
      <CreateAbpDictionaryType
        @reloadType="reloadType"
        @register="registerCreateType"
      ></CreateAbpDictionaryType>
      <CreateAbpDictionary @register="registerCreateModal" @reload="reload" />
      <EditAbpDictionary @register="registerEditModal" @reload="reload" />
    </PageWrapper>
  </div>
</template>

<script lang="ts">
  import { defineComponent, ref } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { PageWrapper } from '/@/components/Page';
  import { BasicModal, useModal } from '/@/components/Modal';
  import CreateAbpDictionary from './CreateAbpDictionary.vue';
  import EditAbpDictionary from './EditAbpDictionary.vue';
  import CreateAbpDictionaryType from './CreateAbpDictionaryType.vue';

  import { useI18n } from '/@/hooks/web/useI18n';
  import {
    tableColumns,
    searchFormSchema,
    getDictionaryTypeAsync,
    dictionaryTypeTableColumns,
    searchDictionaryFormSchema,
    getDictionaryDetailsAsync,
  } from './AbpDictionary';

  import { Tag, message } from 'ant-design-vue';
  export default defineComponent({
    name: 'AbpDictionary',
    components: {
      BasicTable,
      BasicModal,
      PageWrapper,
      TableAction,
      Tag,
      CreateAbpDictionaryType,
      CreateAbpDictionary,
      EditAbpDictionary,
    },
    setup() {
      const { t } = useI18n();

      const [registerCreateModal, { openModal: createModal }] = useModal();
      const [registerEditModal, { openModal: editModal }] = useModal();
      const [registerCreateType, { openModal: createTypeModal }] = useModal();
      const selectedDataDictionaryIdRef = ref('');
      const selectedDataDictionaryDisplayTextRef = ref('');

      //左边表格
      const [registerTypeTable, { reload: reloadType, clearSelectedRowKeys }] = useTable({
        columns: dictionaryTypeTableColumns,
        formConfig: {
          labelWidth: 0,
          schemas: searchDictionaryFormSchema,
          showResetButton: false,
        },
        api: getDictionaryTypeAsync,
        useSearchForm: true,
        showTableSetting: false,
        showIndexColumn: false,
        bordered: true,
        canResize: true,
        rowSelection: { type: 'radio' },
        pagination: false,
      });

      //勾选事件
      const onSelectChange = async ({ rows }) => {
        selectedDataDictionaryIdRef.value = rows[0].id;
        selectedDataDictionaryDisplayTextRef.value = rows[0].displayText;
        reload();
      };

      const handleCreate = () => {
        if (selectedDataDictionaryIdRef.value == '') {
          message.error(t('routes.admin.chooseDictionary'));
          return;
        } else {
          let dictionaryCreate = {
            id: selectedDataDictionaryIdRef.value,
            displayText: selectedDataDictionaryDisplayTextRef.value,
          };
          createModal(true, { dictionaryCreate: dictionaryCreate });
        }
      };

      const [registerTable, { reload }] = useTable({
        columns: tableColumns,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
        },
        api: getDictionaryPageDetailsAsync,
        useSearchForm: true,
        showTableSetting: true,
        showIndexColumn: true,
        bordered: true,
        canResize: true,
        actionColumn: {
          width: 150,
          title: t('common.action'),
          dataIndex: 'action',
          slots: { customRender: 'action' },
        },
      });
      async function getDictionaryPageDetailsAsync(params) {
        if (selectedDataDictionaryIdRef.value == '') {
          return [];
        }
        params.dataDictionaryId = selectedDataDictionaryIdRef.value;
        return await getDictionaryDetailsAsync({ params });
      }
      const handleEdit = (record: Recordable) => {
        editModal(true, {
          record: record,
        });
      };

      const handleCreateType = () => {
        createTypeModal(true);
      };

      return {
        registerTable,
        registerCreateModal,
        registerEditModal,
        handleCreate,
        handleEdit,
        reload,
        registerTypeTable,
        registerCreateType,
        handleCreateType,
        reloadType,
        onSelectChange,
        clearSelectedRowKeys,
        t,
      };
    },
  });
</script>

<style lang="less" scoped></style>

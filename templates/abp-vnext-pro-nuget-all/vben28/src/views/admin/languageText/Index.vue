<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button
          preIcon="ant-design:plus-circle-outlined"
          type="primary"
          @click="openCreateLanguageTextModal"
          v-auth="'AbpIdentity.LanguageTexts.Create'"
        >
          {{ t('common.createText') }}
        </a-button>
      </template>

      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:note-edit-line',
              label: t('common.editText'),
              auth: 'AbpIdentity.LanguageTexts.Update',
              onClick: handleEdit.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>

    <CreateLanguageText
      @register="registerCreateLanguageTextModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
    <UpdateLanguageText
      @register="registerUpdateLanguageTextModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
  </div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicTable, TableAction, useTable } from '/@/components/Table';
  import { tableColumns, searchFormSchema, pageAsync } from './Index';
  import { useModal } from '/@/components/Modal';
  import CreateLanguageText from './CreateLanguageText.vue';
  import UpdateLanguageText from './UpdateLanguageText.vue';
  import { useI18n } from '/@/hooks/web/useI18n';
  export default defineComponent({
    name: 'LanguageText',
    components: {
      BasicTable,
      TableAction,
      CreateLanguageText,
      UpdateLanguageText,
    },
    setup() {
      const { t } = useI18n();
      // table配置
      const [registerTable, { reload, getForm }] = useTable({
        columns: tableColumns,
        formConfig: {
          labelWidth: 70,
          schemas: searchFormSchema,
        },
        api: pageAsync,
        showTableSetting: true,
        useSearchForm: true,
        bordered: true,
        canResize: true,
        showIndexColumn: true,
        immediate: true,
        scroll: { x: true },
        actionColumn: {
          width: 220,
          title: t('common.action'),
          dataIndex: 'action',
          slots: { customRender: 'action' },
        },
      });
      const [registerCreateLanguageTextModal, { openModal: openCreateLanguageTextModal }] =
        useModal();
      const [registerUpdateLanguageTextModal, { openModal: openUpdateLanguageTextModal }] =
        useModal();

      function handleEdit(record: Recordable) {
        let fieldValue = getForm().getFieldsValue();
        openUpdateLanguageTextModal(true, {
          record: record,
          cultureName: fieldValue.cultureName,
        });
      }

      return {
        registerTable,
        reload,
        handleEdit,
        registerCreateLanguageTextModal,
        registerUpdateLanguageTextModal,
        openCreateLanguageTextModal,
        t,
      };
    },
  });
</script>

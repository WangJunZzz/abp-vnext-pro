<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button
          preIcon="ant-design:plus-circle-outlined"
          type="primary"
          @click="openCreateLanguageModal"
          v-auth="'AbpIdentity.Languages.Create'"
        >
          {{ t('common.createText') }}
        </a-button>
      </template>
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'cultureName'">
          {{ record.cultureName }}
          <Tag v-if="record.isDefault" color="green">{{ t('common.default') }}</Tag>
        </template>

        <template v-if="column.key === 'isEnabled'">
          <Tag :color="record.isEnabled ? 'green' : 'red'">
            {{ record.isEnabled ? t('common.enabled') : t('common.disEnabled') }}
          </Tag>
        </template>
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:note-edit-line',
              label: t('common.editText'),
              auth: 'AbpIdentity.Languages.Update',
              onClick: handleEdit.bind(null, record),
            },
            {
              icon: 'ant-design:delete-outlined',
              color: 'error',
              label: t('common.delText'),
              auth: 'AbpIdentity.Languages.Update',
              popConfirm: {
                title: t('common.askDelete'),
                placement: 'left',
                confirm: handleDelete.bind(null, record),
              },
            },
          ]"
        />
      </template>
    </BasicTable>

    <CreateLanguage
      @register="registerCreateLanguageModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
    <UpdateLanguage
      @register="registerUpdateLanguageModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
  </div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { Tag } from 'ant-design-vue';
  import { BasicTable, TableAction, useTable } from '/@/components/Table';
  import { tableColumns, searchFormSchema, pageAsync, deleteAsync } from './Index';
  import { useModal } from '/@/components/Modal';
  import CreateLanguage from './CreateLanguage.vue';
  import UpdateLanguage from './UpdateLanguage.vue';
  import { useI18n } from '/@/hooks/web/useI18n';
  export default defineComponent({
    name: 'Language',
    components: {
      BasicTable,
      TableAction,
      CreateLanguage,
      UpdateLanguage,
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
      const [registerCreateLanguageModal, { openModal: openCreateLanguageModal }] = useModal();
      const [registerUpdateLanguageModal, { openModal: openUpdateLanguageModal }] = useModal();

      function handleEdit(record: Recordable) {
        openUpdateLanguageModal(true, {
          record: record,
        });
      }

      async function handleDelete(record: Recordable) {
        await deleteAsync({ id: record.id });
        await reload();
      }

      return {
        t,
        registerTable,
        reload,
        handleEdit,
        handleDelete,
        registerCreateLanguageModal,
        registerUpdateLanguageModal,
        openCreateLanguageModal,
      };
    },
  });
</script>

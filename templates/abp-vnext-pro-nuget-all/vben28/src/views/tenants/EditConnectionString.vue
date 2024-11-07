<template>
  <BasicModal :title="t('common.editText')" :canFullscreen="false" :showCancelBtn="false" @register="registerModal"
    @ok="submit" :defaultFullscreen="true">

    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button type="primary" preIcon="ant-design:plus-circle-outlined"
          @click="handlerOpenCreateConnectionStringModal">
          {{ t('common.createOrUpdateText') }}
        </a-button>
      </template>

      <template #action="{ record }">
        <TableAction :actions="[

    {
      icon: 'ant-design:minus-outlined',
      auth: 'AbpTenantManagement.Tenants.Delete',
      label: t('common.delText'),
      onClick: handleDelete.bind(null, record),
    },
  ]" />
      </template>
    </BasicTable>

    <CreateConnectionString @register="registerCreateConnectionStringModal" @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }" />
  </BasicModal>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { BasicModal, useModalInner } from '/@/components/Modal';
import { updateConnectionStringFormSchema, editConnectionStringtableColumns, pageConnectionStringAsync, deleteConnectionString } from '/@/views/tenants/Tenant';
import { useI18n } from '/@/hooks/web/useI18n';
import { BasicTable, useTable, TableAction } from '/@/components/Table';
import CreateConnectionString from './CreateConnectionString.vue';
import { useModal } from '/@/components/Modal';
import { useMessage } from '/@/hooks/web/useMessage';
export default defineComponent({
  name: 'EditTenant',
  components: {
    BasicModal,
    BasicTable,
    TableAction,
    CreateConnectionString
  },
  setup() {
    const { t } = useI18n();
    const [registerTable, { reload, getForm }] = useTable({
      columns: editConnectionStringtableColumns,
      formConfig: {
        labelWidth: 100,
        schemas: updateConnectionStringFormSchema,
        showResetButton: false
      },
      api: pageConnectionStringAsync,
      useSearchForm: true,
      showTableSetting: true,
      bordered: true,
      canResize: true,
      showIndexColumn: true,
      actionColumn: {
          title: t('common.action'),
          dataIndex: 'action',
          slots: {
            customRender: 'action',
          },
          width: 350,
          fixed: 'right',
        },
    });


    const [registerCreateConnectionStringModal, { openModal: openCreateConnectionStringModal }] =
      useModal();


    const [registerModal, { closeModal }] = useModalInner((data) => {

      getForm().setFieldsValue({
        id: data.record.id,
      })
    });

    // 编辑
    const handlerOpenCreateConnectionStringModal = () => {
      openCreateConnectionStringModal(true, { id: getForm().getFieldsValue().id });
    };
    const submit = async () => {
      closeModal();
    };

    const { createConfirm } = useMessage();
    // 删除
    const handleDelete = async (record: Recordable) => {
      console.log(record);
      console.log(getForm().getFieldsValue().id);
      let msg = t('common.askDelete');
      createConfirm({
        iconType: 'warning',
        title: t('common.tip'),
        content: msg,
        onOk: async () => {
          await deleteConnectionString(getForm().getFieldsValue().id, record.name);
          await reload();
        },
      });
    };
    return {
      t,
      registerTable,
      registerModal,
      submit,
      registerCreateConnectionStringModal,
      reload,
      handlerOpenCreateConnectionStringModal,
      handleDelete
    };
  },
});
</script>

<style lang="less" scoped></style>

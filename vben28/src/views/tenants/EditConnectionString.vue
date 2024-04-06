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
    </BasicTable>

    <CreateConnectionString @register="registerCreateConnectionStringModal" @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }" />
  </BasicModal>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { BasicModal, useModalInner } from '/@/components/Modal';
import { updateConnectionStringFormSchema, editConnectionStringtableColumns, pageConnectionStringAsync } from '/@/views/tenants/Tenant';
import { useI18n } from '/@/hooks/web/useI18n';
import { BasicTable, useTable, TableAction } from '/@/components/Table';
import CreateConnectionString from './CreateConnectionString.vue';
import { useModal } from '/@/components/Modal';
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
      showIndexColumn: true
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
    return {
      t,
      registerTable,
      registerModal,
      submit,
      registerCreateConnectionStringModal,
      reload,
      handlerOpenCreateConnectionStringModal
    };
  },
});
</script>

<style lang="less" scoped></style>

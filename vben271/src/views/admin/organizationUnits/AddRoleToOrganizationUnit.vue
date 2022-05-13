<template>
  <div>
    <BasicModal
      :title="t('common.createText')"
      :canFullscreen="false"
      @ok="submit"
      :maskClosable="false"
      @cancel="cancel"
      @register="registerModal"
      :minHeight="100"
      :destroyOnClose="true"
    >
      <BasicTable @register="registerRoleTable" size="small"/>
    </BasicModal>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { BasicModal, useModalInner } from "/@/components/Modal";
import { BasicForm } from "/@/components/Form/index";
import { useI18n } from "/@/hooks/web/useI18n";
import {
  getUnAddRolesAsync, addRoleTableColumns, searchAddRoleFormSchema
} from "/@/views/admin/organizationUnits/OrganizationUnit";
import { AddRoleToOrganizationUnitInput } from "/@/services/ServiceProxies";
import { useTable } from "/@/components/Table";

export default defineComponent({
  name: "AddRoleToOrganizationUnit",
  components: {
    BasicModal,
    BasicForm
  },

  setup(_, { emit }) {
    const { t } = useI18n();
    const [registerModal, { closeModal, changeOkLoading }] = useModalInner((data) => {
      getForm().setFieldsValue({
        organizationUnitId: data.organizationUnitId
      });
    });
    const [registerRoleTable, {  getForm,getSelectRowKeys }] = useTable({
      columns: addRoleTableColumns,
      formConfig: {
        labelWidth: 70,
        schemas: searchAddRoleFormSchema
      },
      api: getUnAddRolesAsync,
      showTableSetting: true,
      useSearchForm: false,
      bordered: true,
      canResize: true,
      showIndexColumn: true
    });
    const submit = async () => {
      try {
        let selectedRoles = getSelectRowKeys();
        console.log(selectedRoles);
        let request = new AddRoleToOrganizationUnitInput();
        request.organizationUnitId = getForm().getFieldsValue().organizationUnitId;

        emit("reload");
      } catch (error) {
        changeOkLoading(false);
      }
    };

    const cancel = () => {
      closeModal();
    };

    return {
      registerModal,
      registerRoleTable,
      submit,
      t,
      cancel
    };
  }
});
</script>

<style lang="less" scoped></style>

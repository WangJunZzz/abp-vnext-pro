<template>
  <div>
    <BasicModal
      :title="t('common.createText')"
      :canFullscreen="false"
      @ok="submit"
      :maskClosable="false"
      @cancel="cancel"
      @register="registerModal"
      :destroyOnClose="true"
    >
      <BasicTable @register="registerRoleTable" @selection-change="onSelectChange" size="small" />
    </BasicModal>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { BasicModal, useModalInner } from "/@/components/Modal";
import { BasicForm } from "/@/components/Form/index";
import { useI18n } from "/@/hooks/web/useI18n";
import {
  addUserTableColumns, searchUserFormSchema, addUserToOrganizationUnitAsync, GetUnAddUserAsync
} from "/@/views/admin/organizationUnits/OrganizationUnit";
import { AddUserToOrganizationUnitInput, GetUnAddUserInput } from "/@/services/ServiceProxies";
import { BasicTable, useTable } from "/@/components/Table";

export default defineComponent({
  name: "AddUserToOrganizationUnit",
  components: {
    BasicModal,
    BasicForm,
    BasicTable
  },

  setup(_, { emit }) {
    const { t } = useI18n();
    const getTableAsync = async () => {
      let request= new GetUnAddUserInput();
      request.organizationUnitId=organizationUnitId;
      request.filter=getForm().getFieldsValue().filter;
      return await GetUnAddUserAsync(request)
    };

    const [registerRoleTable, { reload,getForm }] = useTable({
      columns: addUserTableColumns,
      formConfig: {
        labelWidth: 70,
        schemas: searchUserFormSchema,
        showResetButton: false
      },
      api: getTableAsync,
      showTableSetting: false,
      useSearchForm: true,
      bordered: true,
      showIndexColumn: true,
      maxHeight: 400,
      immediate: false,
      rowSelection: { type: "checkbox" }
    });


    let organizationUnitId = "";
    const [registerModal, { closeModal, changeOkLoading }] = useModalInner(async (data) => {
      organizationUnitId = data.organizationUnitId;
      await reload({ searchInfo: { organizationUnitId: organizationUnitId } });
    });


    //勾选事件
    let selectUsers: string[] = [];
    const onSelectChange = ({ rows }) => {
      selectUsers = rows.map((item) => {
        return item.id;
      });

    };
    const submit = async () => {
      try {
        changeOkLoading(true);
        let request = new AddUserToOrganizationUnitInput();
        request.organizationUnitId = organizationUnitId;
        request.userId = selectUsers;
        await addUserToOrganizationUnitAsync(request);
        changeOkLoading(false);
        closeModal();
        emit("reload");
      } catch (error) {
        changeOkLoading(false);
      }
    };

    const cancel = () => {
      organizationUnitId = "";
      selectUsers = [];
      closeModal();
    };

    return {
      registerModal,
      registerRoleTable,
      submit,
      t,
      cancel,
      onSelectChange
    };
  }
});
</script>

<style lang="less" scoped></style>

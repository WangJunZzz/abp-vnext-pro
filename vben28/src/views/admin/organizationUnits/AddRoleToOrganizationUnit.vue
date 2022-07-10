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
  getUnAddRolesAsync, addRoleTableColumns, searchAddRoleFormSchema, addRoleToOrganizationUnitAsync
} from "/@/views/admin/organizationUnits/OrganizationUnit";
import { AddRoleToOrganizationUnitInput, GetUnAddRoleInput } from "/@/services/ServiceProxies";
import { BasicTable, useTable } from "/@/components/Table";

export default defineComponent({
  name: "AddRoleToOrganizationUnit",
  components: {
    BasicModal,
    BasicForm,
    BasicTable
  },

  setup(_, { emit }) {
    const { t } = useI18n();
    const getTableAsync = async () => {
      let request= new GetUnAddRoleInput();
      request.organizationUnitId=organizationUnitId;
      return await getUnAddRolesAsync(request)
    };
    const [registerRoleTable, {  reload }] = useTable({
      columns: addRoleTableColumns,
      formConfig: {
        labelWidth: 70,
        schemas: searchAddRoleFormSchema
      },
      api: getTableAsync,
      showTableSetting: false,
      useSearchForm: false,
      bordered: true,
      showIndexColumn: true,
      maxHeight: 400,
      immediate: false,
      rowSelection: { type: "checkbox" }
    });

    let organizationUnitId = "";
    const [registerModal, { closeModal, changeOkLoading }] = useModalInner(async (data) => {
      organizationUnitId = data.organizationUnitId;
      await reload();
    });
    //勾选事件
    let selectRoles:string[]=[];
    const onSelectChange = ({ rows }) => {
       selectRoles = rows.map((item)=>{
        return item.id;
      })
    };
    const submit = async () => {
      try {
        changeOkLoading(true)
        let request = new AddRoleToOrganizationUnitInput();
        request.organizationUnitId = organizationUnitId;
        request.roleId=selectRoles;
        await addRoleToOrganizationUnitAsync(request);
        changeOkLoading(false);
        closeModal();
        emit("reload");
      } catch (error) {
        changeOkLoading(false);
      }
    };

    const cancel = () => {
      organizationUnitId='';
      selectRoles=[]
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

import {
  OrganizationUnitsServiceProxy,
  IdInput,
  GetOrganizationUnitUserInput,
  GetOrganizationUnitRoleInput,
  RemoveUserToOrganizationUnitInput,
  RemoveRoleToOrganizationUnitInput,
  AddRoleToOrganizationUnitInput,
  AddUserToOrganizationUnitInput,
  GetUnAddRoleInput,
  GetUnAddUserInput
} from "/@/services/ServiceProxies";
import { FormSchema } from "/@/components/Table";
import { BasicColumn } from "/@/components/Table";
import { useI18n } from "/@/hooks/web/useI18n";

const { t } = useI18n();

export const createOrganizationUnitFormSchema: FormSchema[] = [
  {
    field: "parentDisplayName",
    label: t("routes.admin.parentOrganizationUnitName"),
    component: "Input",
    componentProps: {
      disabled: true
    },
    colProps: {
      span: 18
    }
  },
  {
    field: "displayName",
    label: t("routes.admin.organizationUnitName"),
    component: "Input",
    colProps: {
      span: 18
    }
  },
  {
    field: "parentId",
    label: "",
    component: "Input",
    colProps: {
      span: 18
    },
    ifShow: false
  }
];

export const editOrganizationUnitFormSchema: FormSchema[] = [
  {
    field: "displayName",
    label: t("routes.admin.organizationUnitName"),
    component: "Input",
    colProps: {
      span: 18
    }
  },
  {
    field: "id",
    label: "",
    component: "Input",
    colProps: {
      span: 18
    },
    ifShow: false
  }
];
export const addRoleToOrganizationUnitFormSchema: FormSchema[] = [
  {
    field: "displayName",
    label: t("routes.admin.organizationUnitName"),
    component: "Input",
    colProps: {
      span: 18
    }
  },
  {
    field: "organizationUnitId",
    label: "",
    component: "Input",
    colProps: {
      span: 18
    },
    ifShow: false
  }
];
export const userTableColumns: BasicColumn[] = [
  {
    title: t("routes.admin.userManagement_userName"),
    dataIndex: "userName"
  },
  {
    title: t("routes.admin.userManagement_email"),
    dataIndex: "email"
  }
];

export const addUserTableColumns: BasicColumn[] = [
  {
    title: t("routes.admin.userManagement_userName"),
    dataIndex: "userName"
  },
  {
    title: t("routes.admin.userManagement_email"),
    dataIndex: "email"
  }
];

export const roleTableColumns: BasicColumn[] = [
  {
    title: t("routes.admin.userManagement_roleName"),
    dataIndex: "name"
  }
];
export const addRoleTableColumns: BasicColumn[] = [
  {
    title: t("routes.admin.userManagement_roleName"),
    dataIndex: "name"
  }
];
export const searchAddRoleFormSchema: FormSchema[] = [
  {
    field: "filter",
    label: t("routes.admin.userManagement_roleName"),
    component: "Input",
    colProps: { span: 8 }
  },
  {
    field: "organizationUnitId",
    label: "",
    component: "Input",
    ifShow: false,
    colProps: { span: 8 }
  }
];

export const searchUserFormSchema: FormSchema[] = [
  {
    field: "filter",
    label: t("routes.admin.userManagement_userName"),
    component: "Input",
    colProps: { span: 10 }
  },
  {
    field: "organizationUnitId",
    label: "",
    component: "Input",
    ifShow: false,
    colProps: { span: 8 }
  }
];

export async function getTreeAsync() {
  const _organizationUnitsServiceProxy = new OrganizationUnitsServiceProxy();
  return await _organizationUnitsServiceProxy.tree();
}

export async function deleteTreeNodeAsync({ id }) {
  const _organizationUnitsServiceProxy = new OrganizationUnitsServiceProxy();
  const request = new IdInput();
  request.id = id;
  return await _organizationUnitsServiceProxy.delete(request);
}

export async function createOrganizationUnitAsync({
  request,
  changeOkLoading,
  closeModal,
  validate,
  resetFields
}) {
  changeOkLoading(true);
  await validate();
  const _organizationUnitsServiceProxy = new OrganizationUnitsServiceProxy();
  await _organizationUnitsServiceProxy.create(request);
  resetFields();
  changeOkLoading(false);
  closeModal();
}

export async function editOrganizationUnitAsync({
  request,
  changeOkLoading,
  closeModal,
  validate
}) {
  changeOkLoading(true);
  await validate();
  const _organizationUnitsServiceProxy = new OrganizationUnitsServiceProxy();
  await _organizationUnitsServiceProxy.update(request);
  changeOkLoading(false);
  closeModal();
}

export async function getUserTableListAsync(params: GetOrganizationUnitUserInput) {
  const _organizationUnitsServiceProxy = new OrganizationUnitsServiceProxy();
  return _organizationUnitsServiceProxy.getUsers(params);
}

export async function getRoleTableListAsync(params: GetOrganizationUnitRoleInput) {
  const _organizationUnitsServiceProxy = new OrganizationUnitsServiceProxy();
  return _organizationUnitsServiceProxy.getRoles(params);
}


export async function removeUserFromOrganizationUnitAsync(params: RemoveUserToOrganizationUnitInput) {
  const _organizationUnitsServiceProxy = new OrganizationUnitsServiceProxy();
  return _organizationUnitsServiceProxy.removeUserFromOrganizationUnit(params);
}

export async function removeRoleFromOrganizationUnitAsync(params: RemoveRoleToOrganizationUnitInput) {
  const _organizationUnitsServiceProxy = new OrganizationUnitsServiceProxy();
  return _organizationUnitsServiceProxy.removeRoleFromOrganizationUnit(params);
}

export async function addRoleToOrganizationUnitAsync(params: AddRoleToOrganizationUnitInput) {
  const _organizationUnitsServiceProxy = new OrganizationUnitsServiceProxy();
  return _organizationUnitsServiceProxy.addRoleToOrganizationUnit(params);
}

export async function addUserToOrganizationUnitAsync(params: AddUserToOrganizationUnitInput) {
  const _organizationUnitsServiceProxy = new OrganizationUnitsServiceProxy();
  return _organizationUnitsServiceProxy.addUserToOrganizationUnit(params);
}

export async function GetUnAddUserAsync(params: GetUnAddUserInput) {
  const _organizationUnitsServiceProxy = new OrganizationUnitsServiceProxy();
  return _organizationUnitsServiceProxy.getUnAddUsers(params);
}


export async function getUnAddRolesAsync(params: GetUnAddRoleInput) {
  const _organizationUnitsServiceProxy = new OrganizationUnitsServiceProxy();
  return _organizationUnitsServiceProxy.getUnAddRoles(params);
}

import { FormSchema } from "/@/components/Table";
import { BasicColumn } from "/@/components/Table";
import {
  RolesServiceProxy,
  PagingRoleListInput,
  IdentityRoleDtoPagedResultDto,
  IdentityRoleCreateDto,
  PermissionsServiceProxy,
  IdInput,
  GetPermissionInput
} from "/@/services/ServiceProxies";
import { message } from "ant-design-vue";
import { useLoading } from "/@/components/Loading";

import { useI18n } from "/@/hooks/web/useI18n";

const { t } = useI18n();
const [openFullLoading, closeFullLoading] = useLoading({
  tip: "Loading..."
});
export const tableColumns: BasicColumn[] = [
  {
    title: t("routes.admin.userManagement_roleName"),
    dataIndex: "name"
  },
  {
    title: t("routes.admin.roleManagement_default"),
    dataIndex: "isDefault"
  }
];

export const searchFormSchema: FormSchema[] = [
  {
    field: "filter",
    label: t("routes.admin.userManagement_roleName"),
    component: "Input",
    colProps: { span: 8 }
  }
];

export const createFormSchema: FormSchema[] = [
  {
    field: "name",
    label: t("routes.admin.userManagement_roleName"),
    component: "Input",
    required: true,
    colProps: { span: 18 }
  },
  {
    field: "isDefault",
    component: "RadioGroup",
    label: t("routes.admin.roleManagement_default"),
    required: true,
    colProps: {
      span: 18
    },
    defaultValue: "0",
    componentProps: {
      options: [
        {
          label: t("common.true"),
          value: "1"
        },
        {
          label: t("common.false"),
          value: "0"
        }
      ]
    }
  }
];

export const editFormSchema: FormSchema[] = [
  {
    field: "name",
    label: t("routes.admin.userManagement_roleName"),
    component: "Input",
    required: true,
    colProps: { span: 18 }
  },
  {
    field: "isDefault",
    component: "RadioGroup",
    label: t("routes.admin.roleManagement_default"),
    required: true,
    colProps: {
      span: 18
    },
    componentProps: {
      options: [
        {
          label: t("common.true"),
          value: "1"
        },
        {
          label: t("common.false"),
          value: "0"
        }
      ]
    }
  }
];

/**
 * 分页列表
 * @param params
 * @returns
 */
export async function getTableListAsync(params: PagingRoleListInput): Promise<IdentityRoleDtoPagedResultDto | undefined> {
  const _roleServiceProxy = new RolesServiceProxy();
  return _roleServiceProxy.page(params);
}

/**
 * 删除角色
 * @param param0
 */
export async function deleteRoleAsync({ roleId, reload }) {
  try {
    const _roleServiceProxy = new RolesServiceProxy();
    openFullLoading();
    const request = new IdInput();
    request.id = roleId;
    await _roleServiceProxy.delete(request);
    closeFullLoading();
    message.success(t("common.operationSuccess"));
    reload();
  } catch (error) {
    closeFullLoading();
  }
}

/**
 * 创建角色
 * @param param0
 */
export async function createRoleAsync({ request, changeOkLoading, validate, closeModal }) {
  changeOkLoading(true);
  await validate();
  let requestBody: IdentityRoleCreateDto = new IdentityRoleCreateDto();
  requestBody.name = request.name;
  request.isDefault == "1" ? (requestBody.isDefault = true) : (requestBody.isDefault = false);
  const _roleServiceProxy = new RolesServiceProxy();
  await _roleServiceProxy.create(requestBody);
  changeOkLoading(false);
  message.success(t("common.operationSuccess"));
  closeModal();
}

/**
 * 获取角色权限
 * @param roleName
 * @returns
 */
export async function getRolePermissionAsync(roleName: string) {
  const _permissionsServiceProxy = new PermissionsServiceProxy();
  const request = new GetPermissionInput();
  request.providerName = "R";
  request.providerKey = roleName;
  return await _permissionsServiceProxy.tree(request);
}

/**
 * 编辑角色权限
 * @param param0
 */
export async function updateRolePermissionAsync({ request, closeDrawer, setDrawerProps }) {
  setDrawerProps({ loading: true });
  const _permissionsServiceProxy = new PermissionsServiceProxy();
  await _permissionsServiceProxy.update(request);
  setDrawerProps({ loading: false });
  //message.success(t('common.operationSuccess'));
  closeDrawer();
}

/**
 * 编辑角色
 * @param param0
 */
export async function updateRoleAsync({ request, changeOkLoading, validate, closeModal }) {
  await validate();
  changeOkLoading(true);
  const _roleServiceProxy = new RolesServiceProxy();
  await _roleServiceProxy.update(request);
  changeOkLoading(false);
  closeModal();
}


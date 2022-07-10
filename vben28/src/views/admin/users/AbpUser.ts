import { FormSchema } from "/@/components/Table";
import { BasicColumn } from "/@/components/Table";
import { dateUtil } from '/@/utils/dateUtil';
import {
  PagingUserListInput,
  UsersServiceProxy,
  IdentityUserDtoPagedResultDto,
  IdentityRoleDtoListResultDto,
  RolesServiceProxy,
  LockUserInput,
  IdInput
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
    title: t("routes.admin.userManagement_userName"),
    dataIndex: "userName"
  },
  {
    title: t("routes.admin.userManagement_name"),
    dataIndex: "name"
  },

  {
    title: t("routes.admin.userManagement_email"),
    dataIndex: "email"
  },
  {
    title: t("routes.admin.userManagement_phone"),
    dataIndex: "phoneNumber"
  },
  {
    title: t("common.status"),
    dataIndex: "isActive"
  },
  {
    title: t("routes.admin.userManagement_createTime"),
    dataIndex: "creationTime",
    customRender: ({ text }) => {
      return dateUtil(text).format("YYYY-MM-DD HH:mm:ss");
    }
  }
];

export const searchFormSchema: FormSchema[] = [
  {
    field: "filter",
    label: t("routes.admin.userManagement_userName"),
    component: "Input",
    colProps: { span: 8 }
  }
];

export const createFormSchema: FormSchema[] = [
  {
    field: "userName",
    component: "Input",
    label: t("routes.admin.userManagement_userName"),
    labelWidth: 85,
    required: true,
    colProps: {
      span: 12
    },
    componentProps: {
      autocomplete: "off"
    }
  },
  {
    field: "name",
    component: "Input",
    label: t("routes.admin.roleManagement_name"),
    labelWidth: 130,
    required: true,
    colProps: {
      span: 12
    },
    componentProps: {
      autocomplete: "off"
    }
  },
  {
    field: "email",
    component: "Input",
    label: t("routes.admin.userManagement_email"),
    required: true,
    labelWidth: 85,
    colProps: {
      span: 12
    }
  },
  {
    field: "phoneNumber",
    component: "Input",
    label: t("routes.admin.userManagement_phone"),
    required: false,
    labelWidth: 130,
    colProps: {
      span: 12
    }
  },
  {
    field: "password",
    component: "InputPassword",
    label: t("routes.admin.userManagement_password"),
    required: true,
    labelWidth: 85,
    colProps: {
      span: 12
    },
    componentProps: {
      autocomplete: "off"
    }
  },
  {
    field: "confirmPassword",
    component: "InputPassword",
    componentProps: {
      autocomplete: "off"
    },
    label: t("routes.admin.userManagement_confirm_password"),
    required: true,
    labelWidth: 130,
    colProps: {
      span: 12
    }
  }
];

export const editFormSchema: FormSchema[] = [
  {
    field: "userName",
    component: "Input",
    label: t("routes.admin.userManagement_userName"),
    labelWidth: 85,
    required: true,
    colProps: {
      span: 12
    },
    componentProps: {
      autocomplete: "off",
      disabled: true
    }
  },
  {
    field: "name",
    component: "Input",
    label: t("routes.admin.userManagement_name"),
    labelWidth: 130,
    required: true,
    colProps: {
      span: 12
    }
  },
  {
    field: "email",
    component: "Input",
    label: t("routes.admin.userManagement_email"),
    required: true,
    labelWidth: 85,
    colProps: {
      span: 12
    }
  },
  {
    field: "phoneNumber",
    component: "Input",
    label: t("routes.admin.userManagement_phone"),
    required: false,
    labelWidth: 130,
    colProps: {
      span: 12
    }
  },
  {
    field: "password",
    component: "InputPassword",
    label: t("routes.admin.userManagement_password"),
    required: false,
    labelWidth: 85,
    colProps: {
      span: 12
    }
  },
  {
    field: "confirmPassword",
    component: "InputPassword",
    label: t("routes.admin.userManagement_confirm_password"),
    required: false,
    labelWidth: 130,
    colProps: {
      span: 12
    }
  }
];

/**
 * 分页列表
 * @param params
 * @returns
 */
export async function getTableListAsync(
  params: PagingUserListInput
): Promise<IdentityUserDtoPagedResultDto> {
  const _userServiceProxy = new UsersServiceProxy();
  return _userServiceProxy.page(params);
}

/**
 * 导出列表
 * @param params
 * @returns
 */
export function exportAsync({ request }) {
  openFullLoading();
  const _userServiceProxy = new UsersServiceProxy();
  _userServiceProxy.export(request).then(res => {
    const a = document.createElement("a");
    a.href = URL.createObjectURL(res.data);
    a.download = "用户列表导出.xlsx";
    a.click();
    closeFullLoading();
  });
}

/**
 * 获取用户角色
 * @param userId
 * @returns
 */
export async function getRolesByUserIdAsync(userId: string): Promise<IdentityRoleDtoListResultDto> {
  const request = new IdInput();
  request.id = userId;
  const _userServiceProxy = new UsersServiceProxy();
  return _userServiceProxy.role(request);
}

/**
 * 获取所有角色
 * @returns
 */
export async function getAllRoleAsync(): Promise<IdentityRoleDtoListResultDto> {
  const _roleServiceProxy = new RolesServiceProxy();
  return _roleServiceProxy.all();
}

/**
 * 创建用户
 * @param param0
 */
export async function createUserAsync({
  request,
  changeOkLoading,
  validate,
  closeModal,
  resetFields
}) {
  changeOkLoading(true);
  await validate();
  if (request.password != request.confirmPassword) {
    message.error("两次密码输入不一致");
    throw new Error("两次密码输入不一致");
  }
  const _userServiceProxy = new UsersServiceProxy();
  await _userServiceProxy.create(request);
  changeOkLoading(false);
  message.success(t("common.operationSuccess"));
  resetFields();
  closeModal();
}

/**
 * 删除用户
 * @param param0
 */
export async function deleteUserAsync({ userId, reload }) {
  try {
    const _userServiceProxy = new UsersServiceProxy();
    openFullLoading();
    const request = new IdInput();
    request.id = userId;
    await _userServiceProxy.delete(request);
    closeFullLoading();
    message.success(t("common.operationSuccess"));
    reload();
  } catch (error) {
    closeFullLoading();
  }
}

/**
 * 编辑用户
 * @param param0
 */
export async function updateUserAsync({
  request,
  changeOkLoading,
  validate,
  closeModal,
  resetFields
}) {
  changeOkLoading(true);
  await validate();

  const _userServiceProxy = new UsersServiceProxy();
  await _userServiceProxy.update(request);
  changeOkLoading(false);
  resetFields();
  message.success(t("common.operationSuccess"));
  closeModal();
}

/**
 * 启用或者禁用用户
 * @param request
 * @returns
 */
export async function lockUserAsync(request: LockUserInput): Promise<void> {
  const _userServiceProxy = new UsersServiceProxy();
  return _userServiceProxy.lock(request);
}



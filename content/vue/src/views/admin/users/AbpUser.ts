import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import moment from 'moment';
import {
  GetUserListInput,
  UserServiceProxy,
  IdentityUserDtoPagedResultDto,
  IdentityRoleDtoListResultDto,
  RoleServiceProxy
} from '/@/services/ServiceProxies';
import { message } from 'ant-design-vue';
import { useLoading } from '/@/components/Loading';


import { useI18n } from '/@/hooks/web/useI18n';
const { t } = useI18n();
const [openFullLoading, closeFullLoading] = useLoading({
  tip: 'Loading...',
});

export const tableColumns: BasicColumn[] = [
  {
    title: t('routes.admin.userManagement_name'),
    dataIndex: 'name',

  },
  {
    title: t('routes.admin.userManagement_userName'),
    dataIndex: 'userName',

  },
  {
    title: t('routes.admin.userManagement_email'),
    dataIndex: 'email',

  },
  // {
  //   title: t('routes.admin.userManagement_phone'),
  //   dataIndex: 'phoneNumber',

  // },
  {
    title: t('routes.admin.userManagement_createTime'),
    dataIndex: 'creationTime',
    customRender: ({ text }) => {
      return moment(text).format("YYYY-MM-DD HH:mm:ss");
    }
  }
];

export const searchFormSchema: FormSchema[] = [
  {
    field: 'filter',
    label: t('routes.admin.userManagement_userName'),
    component: 'Input',
    colProps: { span: 8 },
  }
];

export const createFormSchema: FormSchema[] = [

  {
    field: 'name',
    component: 'Input',
    label: t('routes.admin.userManagement_name'),
    required: true,
    labelWidth: 70,
    colProps: {
      span: 12,
    }
  },
  {
    field: 'userName',
    component: 'Input',
    label: t('routes.admin.userManagement_userName'),
    labelWidth: 70,
    required: true,
    colProps: {
      span: 12,
    }
  },
  {
    field: 'email',
    component: 'Input',
    label: t('routes.admin.userManagement_email'),
    required: true,
    labelWidth: 70,
    colProps: {
      span: 12,
    }
  },
  {
    field: 'password',
    component: 'InputPassword',
    label: t('routes.admin.userManagement_password'),
    required: true,
    labelWidth: 70,
    colProps: {
      span: 12,
    }
  }
];


export const editFormSchema: FormSchema[] = [

  {
    field: 'name',
    component: 'Input',
    label: t('routes.admin.userManagement_name'),
    required: true,
    labelWidth: 70,
    colProps: {
      span: 12,
    }
  },
  {
    field: 'userName',
    component: 'Input',
    label: t('routes.admin.userManagement_userName'),
    labelWidth: 70,
    required: true,
    colProps: {
      span: 12,
    }
  },
  {
    field: 'email',
    component: 'Input',
    label: t('routes.admin.userManagement_email'),
    required: true,
    labelWidth: 70,
    colProps: {
      span: 12,
    }
  }
];

/**
 * 分页列表
 * @param params
 * @returns
 */
export async function getTableListAsync(params: GetUserListInput): Promise<IdentityUserDtoPagedResultDto | undefined> {
  const _userServiceProxy = new UserServiceProxy();
  return _userServiceProxy.list(params);
}

/**
 * 获取用户角色
 * @param userId
 * @returns
 */
export async function getRolesByUserIdAsync(userId: string): Promise<IdentityRoleDtoListResultDto> {
  const _userServiceProxy = new UserServiceProxy();
  return _userServiceProxy.role(userId);
}

/**
 * 获取所有角色
 * @returns
 */
export async function getAllRoleAsync(): Promise<IdentityRoleDtoListResultDto> {
  const _roleServiceProxy = new RoleServiceProxy();
  return _roleServiceProxy.allList();
}

/**
 * 创建用户
 * @param param0
 */
export async function createUserAsync({ request, changeOkLoading, validate, closeModal, resetFields }) {

  changeOkLoading(true);
  await validate();
  const _userServiceProxy = new UserServiceProxy();
  await _userServiceProxy.userPost(request);
  changeOkLoading(false);
  message.success(t('common.operationSuccess'));
  resetFields();
  closeModal();

}

/**
 * 删除用户
 * @param param0
 */
export async function deleteUserAsync({ userId, reload }) {
  try {
    const _userServiceProxy = new UserServiceProxy();
    openFullLoading();
    await _userServiceProxy.userDelete(userId);
    closeFullLoading();
    message.success(t('common.operationSuccess'));
    reload();
  } catch (error) {
    closeFullLoading();
  }
}

/**
 * 编辑用户
 * @param param0
 */
export async function updateUserAsync({ request, changeOkLoading, validate, closeModal }) {

  changeOkLoading(true);
  await validate();
  const _userServiceProxy = new UserServiceProxy();
  await _userServiceProxy.update(request);
  changeOkLoading(false);
  message.success(t('common.operationSuccess'));
  closeModal();

}

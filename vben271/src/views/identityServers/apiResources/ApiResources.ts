import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import { message } from 'ant-design-vue';
import { useI18n } from '/@/hooks/web/useI18n';
const { t } = useI18n();
import {
  ApiResourceServiceProxy,
  PagingApiRseourceListInput,
  IdInput,
  ApiScopeServiceProxy,
} from '/@/services/ServiceProxies';

export const searchFormSchema: FormSchema[] = [
  {
    field: 'filter',
    label: t('common.key'),
    component: 'Input',
    colProps: { span: 8 },
  },
];

export const tableColumns: BasicColumn[] = [
  {
    title: 'Name',
    dataIndex: 'name',
  },
  {
    title: 'DisplayName',
    dataIndex: 'displayName',
  },
  {
    title: t('common.enabled'),
    dataIndex: 'enabled',
    slots: { customRender: 'enabled' },
  },
  {
    title: 'Description',
    dataIndex: 'description',
  },
  {
    title: 'ShowInDiscoveryDocument',
    dataIndex: 'showInDiscoveryDocument',
    slots: { customRender: 'showInDiscoveryDocument' },
  },
];

export const createFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: 'Name',
    component: 'Input',
    required: true,
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'displayName',
    label: 'DisplayName',
    component: 'Input',
    required: true,
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'secret',
    label: 'Secret',
    component: 'Input',
    required: true,
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'description',
    label: 'Description',
    component: 'Input',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'enabled',
    label: 'Enabled',
    component: 'Switch',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'showInDiscoveryDocument',
    label: 'ShowInDiscoveryDocument',
    labelWidth: 200,
    component: 'Switch',
    colProps: { span: 20 },
  },
  // {
  //   field: 'allowedAccessTokenSigningAlgorithms',
  //   label: 'allowedAccessTokenSigningAlgorithms',
  //   component: 'Input',
  //   colProps: { span: 20 },
  // },
];

export const editFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: 'Name',
    component: 'Input',
    required: true,
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'displayName',
    label: 'DisplayName',
    component: 'Input',
    required: true,
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'secret',
    label: 'Secret',
    component: 'InputPassword',
    required: false,
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'description',
    label: 'Description',
    component: 'Input',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'enabled',
    label: 'Enabled',
    component: 'Switch',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'showInDiscoveryDocument',
    label: 'ShowInDiscoveryDocument',
    labelWidth: 200,
    component: 'Switch',
    colProps: { span: 20 },
  },
  // {
  //   field: 'allowedAccessTokenSigningAlgorithms',
  //   label: 'allowedAccessTokenSigningAlgorithms',
  //   component: 'Input',
  //   colProps: { span: 20 },
  // },
];

export const editApiScopeSchema: FormSchema[] = [
  {
    field: 'apiScopes',
    label: '',
    labelWidth: 100,
    component: 'CheckboxGroup',
  },
];

/**
 * 分页列表
 * @param params
 * @returns
 */
export async function getTableListAsync(params: PagingApiRseourceListInput) {
  const _apiResourceServiceProxy = new ApiResourceServiceProxy();
  return _apiResourceServiceProxy.page(params);
}

export async function deleteApiResourceAsync({ id, reload }) {
  const _apiResourceServiceProxy = new ApiResourceServiceProxy();
  const request = new IdInput();
  request.id = id;
  await _apiResourceServiceProxy.delete(request);
  reload();
}
/**
 * 创建ApiResource
 * @param params
 * @returns
 */
export async function createApiResourceAsync({ request, changeOkLoading, validate, closeModal }) {
  changeOkLoading(true);
  await validate();
  const _apiResourceServiceProxy = new ApiResourceServiceProxy();
  await _apiResourceServiceProxy.create(request);
  changeOkLoading(false);
  closeModal();
}

export async function getAllApiScopeAsync() {
  const _apiScopeServiceProxy = new ApiScopeServiceProxy();
  return await _apiScopeServiceProxy.all();
}

export async function updateApiResourceAsync({ request, changeOkLoading, closeModal }) {
  try {
    changeOkLoading(true);
    const _apiResourceServiceProxy = new ApiResourceServiceProxy();
    await _apiResourceServiceProxy.update(request);
    message.success(t('common.operationSuccess'));
    closeModal();
  } catch (error) {
  } finally {
    changeOkLoading(false);
  }
}

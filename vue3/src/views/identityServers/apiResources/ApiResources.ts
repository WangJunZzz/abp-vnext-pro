import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import { ApiResourceServiceProxy, PagingApiRseourceListInput, IdInput } from '/@/services/ServiceProxies';

export const searchFormSchema: FormSchema[] = [
  {
    field: 'filter',
    label: '关键字',
    component: 'Input',
    colProps: { span: 8 },
  },
];

export const tableColumns: BasicColumn[] = [
  {
    title: 'name',
    dataIndex: 'name',
  },
  {
    title: 'displayName',
    dataIndex: 'displayName',
  },
  {
    title: '是否启用',
    dataIndex: 'enabled',
    slots: { customRender: 'enabled' },
  },
  {
    title: 'description',
    dataIndex: 'description',
  },
  {
    title: 'showInDiscoveryDocument',
    dataIndex: 'showInDiscoveryDocument',
    slots: { customRender: 'showInDiscoveryDocument' },
  },
];

export const createFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: 'name',
    component: 'Input',
    required: true,
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'displayName',
    label: 'displayName',
    component: 'Input',
    required: true,
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'secret',
    label: 'secret',
    component: 'Input',
    required: true,
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'description',
    label: 'description',
    component: 'Input',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'enabled',
    label: 'enabled',
    component: 'Switch',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'showInDiscoveryDocument',
    label: 'showInDiscoveryDocument',
    labelWidth: 200,
    component: 'Switch',
    colProps: { span: 20 },
  },
  {
    field: 'allowedAccessTokenSigningAlgorithms',
    label: 'allowedAccessTokenSigningAlgorithms',
    component: 'Input',
    colProps: { span: 20 },
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

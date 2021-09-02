import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import { ApiScopeServiceProxy, PagingApiScopeListInput, IdInput } from '/@/services/ServiceProxies';

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
    title: 'required',
    dataIndex: 'required',
    slots: { customRender: 'required' },
  },
  {
    title: 'emphasize',
    dataIndex: 'emphasize',
    slots: { customRender: 'emphasize' },
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
    field: 'enabled',
    label: 'enabled',
    component: 'Switch',
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
    field: 'required',
    label: 'required',
    component: 'Switch',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'emphasize',
    label: 'emphasize',
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
];
export const editFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: 'name',
    component: 'Input',
    required: true,
    labelWidth: 200,
    colProps: { span: 20 },
    componentProps: {
      disabled: true,
    },
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
    field: 'enabled',
    label: 'enabled',
    component: 'Switch',
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
    field: 'required',
    label: 'required',
    component: 'Switch',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'emphasize',
    label: 'emphasize',
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
];
/**
 * 分页列表
 * @param params
 * @returns
 */
export async function getTableListAsync(params: PagingApiScopeListInput) {
  const _apiScopeServiceProxy = new ApiScopeServiceProxy();
  return _apiScopeServiceProxy.page(params);
}

export async function createApiScopeAsync({ request, changeOkLoading, validate, closeModal }) {
  changeOkLoading(true);
  await validate();
  const _apiScopeServiceProxy = new ApiScopeServiceProxy();
  await _apiScopeServiceProxy.create(request);
  changeOkLoading(false);
  closeModal();
}
/**
 * 删除
 * @param param0
 */
export async function deleteApiScopeAsync({ id, reload }) {
  const _apiScopeServiceProxy = new ApiScopeServiceProxy();
  const request = new IdInput();
  request.id = id;
  await _apiScopeServiceProxy.delete(request);
  reload();
}

export async function editApiScopeAsync({ request, changeOkLoading, validate, closeModal }) {
  try {
    await validate();
    changeOkLoading(true);
    const _apiScopeServiceProxy = new ApiScopeServiceProxy();
    await _apiScopeServiceProxy.update(request);
    changeOkLoading(false);
    closeModal();
  } catch (error) {
  } finally {
    changeOkLoading(false);
  }
}

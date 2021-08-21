import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import { ClientServiceProxy, PagingClientListInput } from '/@/services/ServiceProxies';

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
    title: 'ClientId',
    dataIndex: 'clientId',
  },
  {
    title: 'ClientName',
    dataIndex: 'clientName',
  },
  {
    title: '是否启用',
    dataIndex: 'enabled',
    slots: { customRender: 'enabled' },
  },
  {
    title: 'AccessTokenLifetime',
    dataIndex: 'accessTokenLifetime',
  },
  {
    title: 'AbsoluteRefreshTokenLifetime',
    dataIndex: 'absoluteRefreshTokenLifetime',
  },
  {
    title: 'Description',
    dataIndex: 'description',
  },
];

export const createFormSchema: FormSchema[] = [
  {
    field: 'clientId',
    label: 'ClientId',
    component: 'Input',
    required: true,
    colProps: { span: 18 },
  },
  {
    field: 'clientName',
    label: 'ClientName',
    component: 'Input',
    required: true,
    colProps: { span: 18 },
  },
  {
    field: 'description',
    label: 'Description',
    component: 'Input',
    required: true,
    colProps: { span: 18 },
  },
];

/**
 * 分页列表
 * @param params
 * @returns
 */
export async function getTableListAsync(params: PagingClientListInput) {
  const _clientServiceProxy = new ClientServiceProxy();
  return _clientServiceProxy.page(params);
}

/**
 * 创建client
 * @param params
 * @returns
 */
export async function createClientAsync({ request, changeOkLoading, validate, closeModal }) {
  changeOkLoading(true);
  await validate();
  const _clientServiceProxy = new ClientServiceProxy();
  await _clientServiceProxy.create(request);
  changeOkLoading(false);
  closeModal();
}

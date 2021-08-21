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
/**
 * 分页列表
 * @param params
 * @returns
 */
export async function getTableListAsync(params: PagingClientListInput) {
  const _clientServiceProxy = new ClientServiceProxy();
  return _clientServiceProxy.page(params);
}

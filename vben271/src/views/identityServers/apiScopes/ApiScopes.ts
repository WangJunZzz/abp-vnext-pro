import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import { ApiScopeServiceProxy, PagingApiScopeListInput, IdInput } from '/@/services/ServiceProxies';
import { useI18n } from '/@/hooks/web/useI18n';
const { t } = useI18n();
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
    title: 'Required',
    dataIndex: 'required',
    slots: { customRender: 'required' },
  },
  {
    title: 'Emphasize',
    dataIndex: 'emphasize',
    slots: { customRender: 'emphasize' },
  },
  {
    title: 'ShowInDiscovery',
    dataIndex: 'showInDiscovery',
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
    field: 'enabled',
    label: 'Enabled',
    component: 'Switch',
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
    field: 'required',
    label: 'Required',
    component: 'Switch',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'emphasize',
    label: 'Emphasize',
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
];
export const editFormSchema: FormSchema[] = [
  {
    field: 'id',
    label: 'Id',
    component: 'Input',
    required: true,
    ifShow: false,
  },
  {
    field: 'name',
    label: 'Name',
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
    label: 'DisplayName',
    component: 'Input',
    required: true,
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
    field: 'description',
    label: 'Description',
    component: 'Input',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'required',
    label: 'Required',
    component: 'Switch',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'emphasize',
    label: 'Emphasize',
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

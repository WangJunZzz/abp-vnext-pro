import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import { useI18n } from '/@/hooks/web/useI18n';
const { t } = useI18n();
import { TenantsServiceProxy, PagingTenantInput, IdInput } from '/@/services/ServiceProxies';
export const searchFormSchema: FormSchema[] = [
  {
    field: 'filter',
    component: 'Input',
    label: t('common.key'),
    labelWidth: 80,
    colProps: {
      span: 6,
    },
  },
];
export const tableColumns: BasicColumn[] = [
  {
    title: t('routes.tenant.name'),
    dataIndex: 'name',
  },
];

export const createFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: t('routes.tenant.name'),
    component: 'Input',
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
  {
    field: 'adminEmailAddress',
    label: t('routes.tenant.adminEmailAddress'),
    component: 'Input',
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
  {
    field: 'adminPassword',
    label: t('routes.tenant.adminPassword'),
    component: 'InputPassword',
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
];
export const editFormSchema: FormSchema[] = [
  {
    field: 'id',
    label: 'Id',
    component: 'Input',
    required: true,
    labelWidth: 150,
    show: false,
    colProps: { span: 20 },
  },
  {
    field: 'name',
    label: t('routes.tenant.name'),
    component: 'Input',
    required: true,
    labelWidth: 150,
    colProps: { span: 20 },
  },
];
export const updateConnectionStringFormSchema: FormSchema[] = [
  {
    field: 'id',
    label: 'Id',
    component: 'Input',
    required: true,
    labelWidth: 150,
    show: false,
    colProps: { span: 20 },
  },
  {
    field: 'connectionString',
    label: t('routes.tenant.connectionString'),
    component: 'Input',
    labelWidth: 150,
    colProps: { span: 20 },
  },
];
export async function getTenantListAsync(request: PagingTenantInput) {
  const _tenantsServiceProxy = new TenantsServiceProxy();
  return await _tenantsServiceProxy.page(request);
}
export async function createTenantAsync({ request, changeOkLoading, validate, closeModal }) {
  changeOkLoading(true);
  await validate();
  const _tenantsServiceProxy = new TenantsServiceProxy();
  await _tenantsServiceProxy.create(request);
  changeOkLoading(false);
  closeModal();
}

export async function updateTenantAsync({ request, changeOkLoading, validate, closeModal }) {
  changeOkLoading(true);
  await validate();
  const _tenantsServiceProxy = new TenantsServiceProxy();
  await _tenantsServiceProxy.update(request);
  changeOkLoading(false);
  closeModal();
}

export async function deleteTenantAsync({ id }) {
  const _tenantsServiceProxy = new TenantsServiceProxy();
  let request = new IdInput();
  request.id = id;
  await _tenantsServiceProxy.delete(request);
}
export async function getConnectionStringAsync({ id }) {
  const _tenantsServiceProxy = new TenantsServiceProxy();
  let request = new IdInput();
  request.id = id;
  return await _tenantsServiceProxy.getConnectionString(request);
}
export async function updateConnectionStringAsync({ request }) {
  const _tenantsServiceProxy = new TenantsServiceProxy();
  return await _tenantsServiceProxy.updateConnectionString(request);
}

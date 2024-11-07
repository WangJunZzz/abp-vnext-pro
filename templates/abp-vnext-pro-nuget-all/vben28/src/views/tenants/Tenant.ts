import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import { useI18n } from '/@/hooks/web/useI18n';
const { t } = useI18n();
import { TenantsServiceProxy, PagingTenantInput, IdInput, PageTenantConnectionStringInput, FeaturesServiceProxy, GetFeatureListResultInput,UpdateFeatureInput,UpdateFeaturesDto, DeleteConnectionStringInput} from '/@/services/ServiceProxies';
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

export const createConnectionStringFormSchema: FormSchema[] = [
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
    helpMessage: ['请谨慎修改', '请谨慎修改', '请谨慎修改'],
    component: 'Input',
    labelWidth: 150,
    colProps: { span: 20 },
  },
  {
    field: 'value',
    label: t('routes.tenant.connectionString'),
    helpMessage: ['请检查连接字符串正确性', '请检查连接字符串正确性', '请检查连接字符串正确性'],
    component: 'Input',
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
    field: 'name',
    label: t('routes.tenant.name'),
    component: 'Input',
    labelWidth: 150,
    colProps: { span: 6 },
  },
  {
    field: 'value',
    label: t('routes.tenant.connectionString'),
    component: 'Input',
    labelWidth: 150,
    colProps: { span: 12 },
  },
];


export const editConnectionStringtableColumns: BasicColumn[] = [
  {
    title: t('routes.tenant.name'),
    dataIndex: 'name',
    width: 240,
  },
  {
    title: t('routes.tenant.connectionString'),
    dataIndex: 'value',
   
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
export async function pageConnectionStringAsync( request :PageTenantConnectionStringInput) {
  const _tenantsServiceProxy = new TenantsServiceProxy();
  return await _tenantsServiceProxy.pageConnectionString(request);
}

export async function addOrUpdateConnectionString({ request }) {
  const _tenantsServiceProxy = new TenantsServiceProxy();
  return await _tenantsServiceProxy.addOrUpdateConnectionString(request);
}

export async function getTenantFeatureListAsync(tenantId) {
  const _featuresServiceProxy = new FeaturesServiceProxy();
  const request = new GetFeatureListResultInput();
  request.providerKey = tenantId;
  request.providerName = 'T';
  return await _featuresServiceProxy.list(request);
}

export async function updateTenantFeatureListAsync(tenantId, params) {
  const _featuresServiceProxy = new FeaturesServiceProxy();
  const request = new UpdateFeatureInput();
  request.providerKey = tenantId;
  request.providerName = 'T';
  request.updateFeaturesDto= new UpdateFeaturesDto();
  request.updateFeaturesDto.features=params;
  return await _featuresServiceProxy.update(request);
}

export async function deleteConnectionString(id, name) {
  const _tenantsServiceProxy = new TenantsServiceProxy();
  const request = new DeleteConnectionStringInput();
  request.name=name;
  request.tenantId=id;
  return await _tenantsServiceProxy.deleteConnectionString(request);
}


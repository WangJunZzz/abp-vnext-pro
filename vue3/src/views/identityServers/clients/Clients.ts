import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import { ClientServiceProxy, EnabledInput, IdInput, PagingClientListInput } from '/@/services/ServiceProxies';

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
    colProps: { span: 20 },
  },
  {
    field: 'clientName',
    label: 'ClientName',
    component: 'Input',
    required: true,
    colProps: { span: 20 },
  },
  {
    field: 'description',
    label: 'Description',
    component: 'Input',
    colProps: { span: 20 },
  },
];

export const editBasicDetailSchema: FormSchema[] = [
  {
    field: 'clientId',
    label: 'ClientId',
    component: 'Input',
    required: true,
    labelWidth: 200,
    componentProps: {
      disabled: true,
    },
    colProps: { span: 20 },
  },
  {
    field: 'clientName',
    label: 'ClientName',
    component: 'Input',
    labelWidth: 200,
    required: true,
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
    field: 'clientUri',
    label: 'ClientUri',
    component: 'Input',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'logoUri',
    label: 'LogoUri',
    component: 'Input',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'frontChannelLogoutUri',
    label: 'FrontChannelLogoutUri',
    component: 'Input',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'backChannelLogoutUri',
    label: 'BackChannelLogoutUri',
    component: 'Input',
    labelWidth: 200,
    colProps: { span: 20 },
  },
];

export const editBasicOptionSchema: FormSchema[] = [
  {
    field: 'enabled',
    label: 'enabled',
    labelWidth: 250,
    component: 'Switch',
    colProps: { span: 10 },
  },
  {
    field: 'requireClientSecret',
    label: 'requireClientSecret',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
  {
    field: 'requireConsent',
    label: 'requireConsent',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
  {
    field: 'allowRememberConsent',
    label: 'allowRememberConsent',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },

  {
    field: 'requirePkce',
    label: 'requirePkce',
    labelWidth: 250,
    component: 'Switch',
    colProps: { span: 10 },
  },
  {
    field: 'allowOfflineAccess',
    label: 'allowOfflineAccess',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
  {
    field: 'enableLocalLogin',
    label: 'enableLocalLogin',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
  {
    field: 'includeJwtId',
    label: 'includeJwtId',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
  {
    field: 'allowAccessTokensViaBrowser',
    label: 'allowAccessTokensViaBrowser',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
  {
    field: 'alwaysIncludeUserClaimsInIdToken',
    label: 'alwaysIncludeUserClaimsInIdToken',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },

  {
    field: 'frontChannelLogoutSessionRequired',
    label: 'frontChannelLogoutSessionRequired',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
  {
    field: 'backChannelLogoutSessionRequired',
    label: 'backChannelLogoutSessionRequired',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
];

export const editBasicTokenSchema: FormSchema[] = [
  {
    field: 'accessTokenLifetime',
    label: 'accessTokenLifetime',
    labelWidth: 200,
    component: 'Input',
  },
  {
    field: 'authorizationCodeLifetime',
    label: 'authorizationCodeLifetime',
    labelWidth: 200,
    component: 'Input',
  },
  {
    field: 'absoluteRefreshTokenLifetime',
    label: 'absoluteRefreshTokenLifetime',
    labelWidth: 200,
    component: 'Input',
  },
  {
    field: 'slidingRefreshTokenLifetime',
    label: 'slidingRefreshTokenLifetime',
    labelWidth: 200,
    component: 'Input',
  },
  {
    field: 'refreshTokenExpiration',
    label: 'refreshTokenExpiration',
    labelWidth: 200,
    component: 'Input',
  },
  {
    field: 'deviceCodeLifetime',
    label: 'deviceCodeLifetime',
    labelWidth: 200,
    component: 'Input',
  },
];
export const editBasicSecretSchema: FormSchema[] = [
  {
    field: 'secretType',
    component: 'Select',
    label: 'Secret',
    labelWidth: 100,
    colProps: {
      span: 15,
    },
    componentProps: {
      options: [
        {
          label: 'SharedSecret',
          value: 'SharedSecret',
          key: '1',
        },
        {
          label: 'X509Thumbprint',
          value: 'X509Thumbprint',
          key: '2',
        },
      ],
    },
  },
  {
    field: 'secret',
    label: 'secret',
    labelWidth: 100,
    component: 'InputPassword',
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

/**
 * 删除
 * @param param0
 */
export async function deleteClientAsync({ id, reload }) {
  const _clientServiceProxy = new ClientServiceProxy();
  const request = new IdInput();
  request.id = id;
  await _clientServiceProxy.delete(request);
  reload();
}
/**
 * 删除
 * @param param0
 */
export async function enabledClientAsync({ clientId, enabled, reload }) {
  const _clientServiceProxy = new ClientServiceProxy();
  const request = new EnabledInput();
  request.clientId = clientId;
  request.enabled = enabled;
  await _clientServiceProxy.enabled(request);
  reload();
}

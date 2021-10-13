import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import { message } from 'ant-design-vue';
import { useI18n } from '/@/hooks/web/useI18n';
const { t } = useI18n();
import {
  ClientServiceProxy,
  EnabledInput,
  IdInput,
  PagingClientListInput,
  AddRedirectUriInput,
  RemoveRedirectUriInput,
  AddCorsInput,
  RemoveCorsInput,
  ApiScopeServiceProxy,
  UpdateScopeInput,
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
    title: 'ClientId',
    dataIndex: 'clientId',
  },
  {
    title: 'ClientName',
    dataIndex: 'clientName',
  },
  {
    title: t('common.enabled'),
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
    field: 'allowedGrantTypes',
    label: 'GrantType',
    component: 'Select',
    required: true,
    colProps: { span: 20 },
    componentProps: {
      options: [
        {
          label: 'ClientCredentials',
          value: 'client_credentials',
        },
        {
          label: 'Implicit',
          value: 'implicit',
        },
        {
          label: 'AuthorizationCode',
          value: 'authorization_code',
        },
        {
          label: 'Hybrid',
          value: 'hybrid',
        },
        {
          label: 'ResourceOwnerPassword',
          value: 'password',
        },
      ],
    },
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
    field: 'allowedGrantTypes',
    label: 'GrantType',
    component: 'Select',
    required: true,
    labelWidth: 200,
    colProps: { span: 20 },
    componentProps: {
      options: [
        {
          label: 'ClientCredentials',
          value: 'client_credentials',
        },
        {
          label: 'Implicit',
          value: 'implicit',
        },
        {
          label: 'AuthorizationCode',
          value: 'authorization_code',
        },
        {
          label: 'Hybrid',
          value: 'hybrid',
        },
        {
          label: 'ResourceOwnerPassword',
          value: 'password',
        },
      ],
    },
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
    label: 'Enabled',
    labelWidth: 250,
    component: 'Switch',
    colProps: { span: 10 },
  },
  {
    field: 'requireClientSecret',
    label: 'RequireClientSecret',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
  {
    field: 'requireConsent',
    label: 'RequireConsent',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
  {
    field: 'allowRememberConsent',
    label: 'AllowRememberConsent',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },

  {
    field: 'requirePkce',
    label: 'RequirePkce',
    labelWidth: 250,
    component: 'Switch',
    colProps: { span: 10 },
  },
  {
    field: 'allowOfflineAccess',
    label: 'AllowOfflineAccess',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
  {
    field: 'enableLocalLogin',
    label: 'EnableLocalLogin',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
  {
    field: 'includeJwtId',
    label: 'IncludeJwtId',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
  {
    field: 'allowAccessTokensViaBrowser',
    label: 'AllowAccessTokensViaBrowser',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
  {
    field: 'alwaysIncludeUserClaimsInIdToken',
    label: 'AlwaysIncludeUserClaimsInIdToken',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },

  {
    field: 'frontChannelLogoutSessionRequired',
    label: 'FrontChannelLogoutSessionRequired',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
  {
    field: 'backChannelLogoutSessionRequired',
    label: 'BackChannelLogoutSessionRequired',
    component: 'Switch',
    labelWidth: 250,
    colProps: { span: 10 },
  },
];

export const editBasicTokenSchema: FormSchema[] = [
  {
    field: 'accessTokenLifetime',
    label: 'AccessTokenLifetime',
    labelWidth: 200,
    component: 'Input',
  },
  {
    field: 'identityTokenLifetime',
    label: 'IdentityTokenLifetime',
    labelWidth: 200,
    component: 'Input',
  },
  {
    field: 'authorizationCodeLifetime',
    label: 'AuthorizationCodeLifetime',
    labelWidth: 200,
    component: 'Input',
  },
  {
    field: 'absoluteRefreshTokenLifetime',
    label: 'AbsoluteRefreshTokenLifetime',
    labelWidth: 200,
    component: 'Input',
  },
  {
    field: 'slidingRefreshTokenLifetime',
    label: 'SlidingRefreshTokenLifetime',
    labelWidth: 200,
    component: 'Input',
  },
  {
    field: 'refreshTokenExpiration',
    label: 'RefreshTokenExpiration',
    labelWidth: 200,
    component: 'Input',
  },
  {
    field: 'deviceCodeLifetime',
    label: 'DeviceCodeLifetime',
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

export async function updateClientAsync({ request, changeOkLoading, closeModal }) {
  try {
    changeOkLoading(true);
    const _clientServiceProxy = new ClientServiceProxy();
    await _clientServiceProxy.updateBasic(request);
    message.success(t('common.operationSuccess'));
    closeModal();
  } catch (error) {
  } finally {
    changeOkLoading(false);
  }
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
 * 启用禁用
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

export async function addRedirectUriAsync({ clientId, uri }) {
  const _clientServiceProxy = new ClientServiceProxy();
  let request = new AddRedirectUriInput();
  request.clientId = clientId;
  request.uri = uri;
  return await _clientServiceProxy.addRedirectUri(request);
}

export async function removeRedirectUriAsync({ clientId, uri }) {
  const _clientServiceProxy = new ClientServiceProxy();
  let request = new RemoveRedirectUriInput();
  request.clientId = clientId;
  request.uri = uri;
  return await _clientServiceProxy.removeRedirectUri(request);
}

export async function addLogoutRedirectUriAsync({ clientId, uri }) {
  const _clientServiceProxy = new ClientServiceProxy();
  let request = new RemoveRedirectUriInput();
  request.clientId = clientId;
  request.uri = uri;
  return await _clientServiceProxy.addLogoutRedirectUri(request);
}
export async function removeLogoutRedirectUriAsync({ clientId, uri }) {
  const _clientServiceProxy = new ClientServiceProxy();
  let request = new RemoveRedirectUriInput();
  request.clientId = clientId;
  request.uri = uri;
  return await _clientServiceProxy.removeLogoutRedirectUri(request);
}
export async function addCorsAsync({ clientId, origin }) {
  const _clientServiceProxy = new ClientServiceProxy();
  let request = new AddCorsInput();
  request.clientId = clientId;
  request.origin = origin;
  return await _clientServiceProxy.addCors(request);
}

export async function removeCorsAsync({ clientId, origin }) {
  const _clientServiceProxy = new ClientServiceProxy();
  let request = new RemoveCorsInput();
  request.clientId = clientId;
  request.origin = origin;
  return await _clientServiceProxy.removeCors(request);
}

export async function getAllScopeAsync() {
  const _apiScopeServiceProxy = new ApiScopeServiceProxy();
  return _apiScopeServiceProxy.all();
}

export async function updateScopesAsync({ clientId, scopes }) {
  const _clientServiceProxy = new ClientServiceProxy();
  let request = new UpdateScopeInput();
  request.clientId = clientId;
  request.scopes = scopes;
  return await _clientServiceProxy.updateScopes(request);
}

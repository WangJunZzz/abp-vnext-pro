import { defHttp } from '/@/utils/http/axios';
import {
  LoginParams,
  LoginResultModel,
  GetUserInfoByUserIdParams,
  GetUserInfoByUserIdModel,
  GetUserInfoModel,
} from './model/userModel';

import { ErrorMessageMode } from '/#/axios';
import {
  AccountServiceProxy,
  LoginOutput,
  LoginInput,
  AbpApplicationConfigurationServiceProxy,
} from '/@/services/ServiceProxies';
enum Api {
  Login = '/login',
  Logout = '/logout',
  GetUserInfoById = '/getUserInfoById',
  GetPermCodeByUserId = '/getPermCodeByUserId',
  // GetUserInfo = '/getUserInfo',
  // GetPermCode = '/getPermCode',
}
/**
 * 登录
 * @param input
 * @returns
 */
export function login(input: LoginInput): Promise<LoginOutput> {
  const _loginServiceProxy = new AccountServiceProxy();
  return _loginServiceProxy.login(input);
}

/**
 * sts登录
 * @param token
 * @returns
 */
export function stsLogin(token: string): Promise<LoginOutput> {
  const _accountServiceProxy = new AccountServiceProxy();
  return _accountServiceProxy.sts(token);
}
export function stsLogout() {
  const _accountServiceProxy = new AccountServiceProxy();
  return _accountServiceProxy.logout();
}

/**
 * 获取应用程序配置
 * @returns
 */
export function getAbpApplicationConfiguration() {
  const _abpApplicationConfigurationServiceProxy = new AbpApplicationConfigurationServiceProxy();
  return _abpApplicationConfigurationServiceProxy.applicationConfiguration();
}

/**
 * @description: getUserInfoById
 */
export function getUserInfoById(params: GetUserInfoByUserIdParams) {
  return defHttp.get<GetUserInfoByUserIdModel>({
    url: Api.GetUserInfoById,
    params,
  });
}

export function getPermCodeByUserId(params: GetUserInfoByUserIdParams) {
  return defHttp.get<string[]>({
    url: Api.GetPermCodeByUserId,
    params,
  });
}

/**
 * @description: user login api
 */
export function loginApi(params: LoginParams, mode: ErrorMessageMode = 'modal') {
  return defHttp.post<LoginResultModel>(
    {
      url: Api.Login,
      params,
    },
    {
      errorMessageMode: mode,
    }
  );
}

/**
 * @description: getUserInfo
 */
// export function getUserInfo() {
//   return defHttp.get<GetUserInfoModel>({ url: Api.GetUserInfo });
// }

// export function getPermCode() {
//   return defHttp.get<string[]>({ url: Api.GetPermCode });
// }

export function doLogout() {
  return defHttp.get({ url: Api.Logout });
}

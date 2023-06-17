import { defHttp } from '/@/utils/http/axios';
import {
  LoginParams,
  LoginResultModel,
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
// export function id4(token: string): Promise<LoginOutput> {
//   const _accountServiceProxy = new AccountServiceProxy();
//   return _accountServiceProxy.id4(token);

// }

/**
 * sts登录
 * @param token
 * @returns
 */
// export function github(code: string): Promise<LoginOutput> {

//   const _accountServiceProxy = new AccountServiceProxy();
//   return _accountServiceProxy.github(code);
 
// }

/**
 * 获取应用程序配置
 * @returns
 */
export function getAbpApplicationConfiguration() {
  const _abpApplicationConfigurationServiceProxy = new AbpApplicationConfigurationServiceProxy();
  return _abpApplicationConfigurationServiceProxy.applicationConfiguration();
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

export function doLogout() {
  return defHttp.get({ url: Api.Logout });
}

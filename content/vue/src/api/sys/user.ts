import { defHttp } from '/@/utils/http/axios';
import {
  LoginParams,
  LoginResultModel,
  GetUserInfoByUserIdParams,
  GetUserInfoByUserIdModel,
} from './model/userModel';

import { ErrorMessageMode } from '/@/utils/http/axios/types';
import { LoginServiceProxy, LoginOutputDto, LoginInputDto, AbpApplicationConfigurationServiceProxy } from '/@/services/ServiceProxies'
enum Api {
  Login = '/login',
  GetUserInfoById = '/getUserInfoById',
  GetPermCodeByUserId = '/getPermCodeByUserId',
}

/**
 * 登录
 * @param input
 * @returns
 */
export function login(input: LoginInputDto): Promise<LoginOutputDto> {
  const _loginServiceProxy = new LoginServiceProxy();
  return _loginServiceProxy.login(input);
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

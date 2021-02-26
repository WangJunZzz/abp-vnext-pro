import { LOGIN, ROUTES, AbpUser, AbpApplicationConfiguration, Profile } from '@/services/api'
import { request, METHOD } from '@/utils/request'

/**
 * 登录服务
 * @param name 账户名
 * @param password 账户密码
 * @returns {Promise<AxiosResponse<T>>}
 */
export async function login(name, password) {
  return request(LOGIN, METHOD.POST, {
    name: name,
    password: password
  })
}

/**
 * 新增用户
 */
export async function postAbpUser(params) {
  return request(AbpUser, METHOD.POST, params)
}

/**
 * 编辑用户
 */
export async function putAbpUser(id, params) {
  let url = AbpUser + "/" + id;
  return request(url, METHOD.PUT, params)
}

/**
 * 获取用户角色
 */
export async function getUserRoleById(params) {
  let url = AbpUser + "/" + params + "/roles";
  return request(url, METHOD.GET)
}

/**
 * 获取abp 应用程序配置信息
 */
export async function getAbpApplicationConfiguration() {
  return request(AbpApplicationConfiguration, METHOD.GET);
}


export async function getRoutesConfig() {
  return request(ROUTES, METHOD.GET)
}

/**
 * 退出登录
 */
export function logout() {
  localStorage.removeItem(process.env.VUE_APP_ROUTES_KEY)
  localStorage.removeItem(process.env.VUE_APP_PERMISSIONS_KEY)
  localStorage.removeItem(process.env.VUE_APP_ROLES_KEY)
}

export function getAbpUserList(params) {
  return request(AbpUser, METHOD.GET, params)
}

/**
 * 修改密码
 * @param {*} params 
 */
export function changePassword(params) {
  let url = Profile + "/change-password";
  return request(url, METHOD.POST, params);
}
export default {
  login,
  logout,
  getRoutesConfig,
  getAbpUserList
}

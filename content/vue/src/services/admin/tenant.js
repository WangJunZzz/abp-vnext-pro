import { AbpTenant } from '@/services/api'
import { request, METHOD } from '@/utils/request'


/**
 * 角色管理
 */
export function getAbpTenantList(params) {
  return request(AbpTenant, METHOD.GET, params)
}


/**
 * 新增租户
 */
export function postAbpTenant(params) {
  return request(AbpTenant, METHOD.POST, params)
}


/**
 * 编辑租户
 */
export function putAbpTenant(id, params) {
  let url = AbpTenant + "/" + id;
  return request(url, METHOD.PUT, params)
}


export default {

  getAbpTenantList,
  postAbpTenant,
  putAbpTenant
}

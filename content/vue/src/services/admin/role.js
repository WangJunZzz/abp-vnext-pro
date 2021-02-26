import { AbpRole, AbpRolePermission } from '@/services/api'
import { request, METHOD } from '@/utils/request'


/**
 * 角色管理
 */
export function getAbpRoleList(params) {
  return request(AbpRole, METHOD.GET, params)
}
/**
 * 新增角色
 */
export function postAbpRole(params) {
  return request(AbpRole, METHOD.POST, params);
}
/**
 * 编辑角色
 */
export function putAbpRole(id, params) {
  let url = AbpRole + "/" + id;
  return request(url, METHOD.PUT, params);
}

/**
 * 获取所有角色
 */
export function getAbpRoleListAll() {
  let url = AbpRole + "/all";
  return request(url, METHOD.GET)
}

/**
 * 获取角色权限
 */
export function getAbpRolePermission(params) {
  return request(AbpRolePermission, METHOD.GET, params)
}

/**
 * 更新角色权限
 * @param {角色名称} roleName 
 * @param {权限} params 
 */
export function putAbpRolePermission(roleName, params) {
  let url = AbpRolePermission + "?providerName=R&providerKey=" + roleName
  return request(url, METHOD.PUT, params);
}
export default {
  getAbpRoleList,
  getAbpRoleListAll,
  getAbpRolePermission,
  putAbpRolePermission,
  postAbpRole,
  putAbpRole
}

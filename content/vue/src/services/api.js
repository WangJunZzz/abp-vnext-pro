//跨域代理前缀
// const API_PROXY_PREFIX='/api'
// const BASE_URL = process.env.NODE_ENV === 'production' ? process.env.VUE_APP_API_BASE_URL : API_PROXY_PREFIX
const BASE_URL = process.env.VUE_APP_API_BASE_URL
module.exports = {
  LOGIN: `${BASE_URL}/api/app/login`,
  ROUTES: `${BASE_URL}/routes`,

  /**
   * 获取abp 应用程序信息
   */
  AbpApplicationConfiguration: `${BASE_URL}/api/abp/application-configuration`,

  /**
   * 用户
   */
  AbpUser: `${BASE_URL}/api/identity/users`,


  /**
   * 角色权限
   */
  AbpRolePermission: `${BASE_URL}/api/permission-management/permissions`,

  /**
 * 角色
 */
  AbpRole: `${BASE_URL}/api/identity/roles`,


  /**
   * 租户
   */
  AbpTenant: `${BASE_URL}/api/multi-tenancy/tenants`,

  /**
   * 数据字典
   */
  Dic: `${BASE_URL}/api/app/dic`,

  /**
   * 用户信息
   */
  Profile: `${BASE_URL}/api/identity/my-profile`,

  /**
   * 设置信息
   */
  SettingsUI: `${BASE_URL}/api/settingUi`,
}

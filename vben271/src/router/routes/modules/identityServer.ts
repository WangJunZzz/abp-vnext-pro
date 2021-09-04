import type { AppRouteModule } from '/@/router/types';
import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';
const identityServer: AppRouteModule = {
  path: '/identityServer',
  name: 'IentityServer',
  component: LAYOUT,
  //redirect: '/admin/abpUser',
  meta: {
    orderNo: 30,
    icon: 'ion:grid-outline',
    title: t('routes.admin.identityServer'),
  },
  children: [
    {
      path: 'clients',
      name: 'Clients',
      component: () => import('/@/views/identityServers/clients/Clients.vue'),
      meta: {
        title: t('routes.admin.client'),
        icon: 'ant-design:copyright-circle-outlined',
        policy: 'IdentityServerManagement.Client',
      },
    },
    {
      path: 'apiResources',
      name: 'ApiResources',
      component: () => import('/@/views/identityServers/apiResources/ApiResources.vue'),
      meta: {
        title: t('routes.admin.apiResource'),
        icon: 'ant-design:euro-outlined',
        policy: 'IdentityServerManagement.ApiResource',
      },
    },
    {
      path: 'apiScopes',
      name: 'ApiScopes',
      component: () => import('/@/views/identityServers/apiScopes/ApiScopes.vue'),
      meta: {
        title: t('routes.admin.apiSocpe'),
        icon: 'ant-design:compass-outlined',
        policy: 'IdentityServerManagement.ApiScope',
      },
    },
    {
      path: 'identityResources',
      name: 'IdentityResources',
      component: () => import('/@/views/identityServers/identityResources/IdentityResources.vue'),
      meta: {
        title: t('routes.admin.identityResource'),
        icon: 'ant-design:usergroup-delete-outlined',
        policy: 'IdentityServerManagement.IdentityResources',
      },
    },
  ],
};

export default identityServer;

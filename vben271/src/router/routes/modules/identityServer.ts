import type { AppRouteModule } from '/@/router/types';
import { LAYOUT } from '/@/router/constant';

const identityServer: AppRouteModule = {
  path: '/identityServer',
  name: 'IentityServer',
  component: LAYOUT,
  //redirect: '/admin/abpUser',
  meta: {
    orderNo: 30,
    icon: 'ion:grid-outline',
    title: 'IdentityServer',
  },
  children: [
    {
      path: 'clients',
      name: 'Clients',
      component: () => import('/@/views/identityServers/clients/Clients.vue'),
      meta: {
        title: 'Clients',
        icon: 'ant-design:skin-outlined',
      },
    },
    {
      path: 'apiResources',
      name: 'ApiResources',
      component: () => import('/@/views/identityServers/apiResources/ApiResources.vue'),
      meta: {
        title: 'ApiResources',
        icon: 'ant-design:skin-outlined',
      },
    },
    {
      path: 'apiScopes',
      name: 'ApiScopes',
      component: () => import('/@/views/identityServers/apiScopes/ApiScopes.vue'),
      meta: {
        title: 'ApiScopes',
        icon: 'ant-design:skin-outlined',
      },
    },
    {
      path: 'identityResources',
      name: 'IdentityResources',
      component: () => import('/@/views/identityServers/identityResources/IdentityResources.vue'),
      meta: {
        title: 'IdentityResources',
        icon: 'ant-design:skin-outlined',
      },
    },
  ],
};

export default identityServer;

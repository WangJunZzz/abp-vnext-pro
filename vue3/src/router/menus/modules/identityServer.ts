import type { MenuModule } from '/@/router/types';
import { t } from '/@/hooks/web/useI18n';

const identityServer: MenuModule = {
  orderNo: 20,
  menu: {
    path: '/identityServer',
    name: 'IdentityServer',
    children: [
      {
        path: 'clients',
        name: 'Clients',
        tag: {
          type: 'warn',
          dot: true,
        },
      },
      {
        path: 'apiResources',
        name: 'ApiResources',
        tag: {
          type: 'warn',
          dot: true,
        },
      },
      {
        path: 'apiScopes',
        name: 'ApiScopes',
        tag: {
          type: 'warn',
          dot: true,
        },
      },
      {
        path: 'identityResources',
        name: 'IdentityResources',
        tag: {
          type: 'warn',
          dot: true,
        },
      },
    ],
  },
};
export default identityServer;

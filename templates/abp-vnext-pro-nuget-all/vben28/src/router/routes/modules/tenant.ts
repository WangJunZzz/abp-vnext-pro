import type { AppRouteModule } from '/@/router/types';
import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';
const tenant: AppRouteModule = {
  path: '/tenant',
  name: 'Tenant',
  component: LAYOUT,
  meta: {
    orderNo: 30,
    icon: 'ant-design:contacts-outlined',
    title: t('routes.tenant.tenantManagement'),
    policy: 'AbpTenantManagement',
  },
  children: [
    {
      path: 'Tenant',
      name: 'Tenant',
      component: () => import('/@/views/tenants/Tenant.vue'),
      meta: {
        title: t('routes.tenant.tenantList'),
        icon: 'ant-design:switcher-filled',
        policy: 'AbpTenantManagement.Tenants',
      },
    },
  ],
};

export default tenant;

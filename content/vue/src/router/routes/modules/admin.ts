import type { AppRouteModule } from '/@/router/types';
import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';

const admin: AppRouteModule = {
  path: '/admin',
  name: 'Admin',
  component: LAYOUT,
  meta: {
    icon: 'ion:grid-outline',
    title: t('routes.admin.systemManagement'),
  },
  children: [
    {
      path: 'abpUser',
      name: 'AbpUser',
      component: () => import('/@/views/admin/users/AbpUser.vue'),
      meta: {
        title: t('routes.admin.userManagement'),
        policy: 'AbpIdentity.Users.Query',
        icon: 'ant-design:skin-outlined'
      },
    },
    {
      path: 'abpRole',
      name: 'AbpRole',
      component: () => import('/@/views/admin/roles/AbpRole.vue'),
      meta: {
        title: t('routes.admin.roleManagement'),
        policy: 'AbpIdentity.Roles.Query',
        icon: 'ant-design:lock-outlined'
      },
    },
    {
      path: 'audit',
      name: 'Audit',
      component: () => import('/@/views/admin/audits/AuditLog.vue'),
      meta: {
        title: t('routes.admin.auditManagement'),
        policy: "AbpIdentity.AuditLog.Query",
        icon: 'ant-design:audit-outlined'
      },
    }
  ],
};

export default admin;

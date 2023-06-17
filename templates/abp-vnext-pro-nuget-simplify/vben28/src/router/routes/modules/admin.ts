import type { AppRouteModule } from '/@/router/types';
import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';

const admin: AppRouteModule = {
  path: '/admin',
  name: 'Admin',
  component: LAYOUT,
  //redirect: '/admin/abpUser',
  meta: {
    orderNo: 20,
    icon: 'ion:grid-outline',
    title: t('routes.admin.systemManagement'),
    policy: 'AbpIdentity'
  },
  children: [
    {
      path: 'abpUser',
      name: 'AbpUser',
      component: () => import('/@/views/admin/users/AbpUser.vue'),
      meta: {
        title: t('routes.admin.userManagement'),
        policy: 'AbpIdentity.Users',
        icon: 'ant-design:skin-outlined',
      },
    },
    {
      path: 'abpRole',
      name: 'AbpRole',
      component: () => import('/@/views/admin/roles/AbpRole.vue'),
      meta: {
        title: t('routes.admin.roleManagement'),
        policy: 'AbpIdentity.Roles',
        icon: 'ant-design:lock-outlined',
      },
    },
    {
      path: 'organizationUnit',
      name: 'organizationUnit',
      component: () => import('/@/views/admin/organizationUnits/OrganizationUnit.vue'),
      meta: {
        title: t('routes.admin.organizationUnitManagement'),
        icon: 'ant-design:gold-outlined',
        policy: 'AbpIdentity.OrganizationUnitManagement',
      },
    },
    {
      path: 'settings',
      name: 'Settings',
      component: () => import('/@/views/admin/settings/Setting.vue'),
      meta: {
        title: t('routes.admin.settingManagement'),
        policy: 'AbpIdentity.Setting',
        icon: 'ant-design:unordered-list-outlined',
      },
    },
    {
      path: 'abpAuditLogs',
      name: 'AuditLogs',
      component: () => import('/@/views/admin/auditLog/AuditLog.vue'),
      meta: {
        title: t('routes.admin.auditLog'),
        policy: 'AbpIdentity.AuditLog',
        icon: 'ant-design:snippets-twotone',
      },
    },
    {
      path: 'dataDictionary',
      name: 'dataDictionary',
      component: () => import('/@/views/admin/dictionary/AbpDictionary.vue'),
      meta: {
        title: t('routes.admin.dictionaryManagement'),
        icon: 'ant-design:table-outlined',
        policy: 'AbpIdentity.DataDictionaryManagement',
      },
    }
  ],
};

export default admin;

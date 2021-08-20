import type { MenuModule } from '/@/router/types';
import { t } from '/@/hooks/web/useI18n';

const admin: MenuModule = {
  orderNo: 10,
  menu: {
    path: '/admin',
    name: t('routes.admin.systemManagement'),
    children: [{
      path: 'abpUser',
      name: t('routes.admin.userManagement'),
      tag: {
        type: 'warn',
        dot: true,
      },
    }, {
      path: 'abpRole',
      name: t('routes.admin.roleManagement'),
      tag: {
        dot: true,
        type: 'warn',

      },
    }, {
      path: 'audit',
      name: t('routes.admin.audit'),
      tag: {
        type: 'warn',
        dot: true,
      },
    }, {
      path: 'dictionary',
      name: t('routes.admin.dictionary'),
      tag: {
        type: 'warn',
        dot: true,
      },
    }, {
      path: 'operationLog',
      name:'操作日志(no)',// t('routes.admin.operationLog')
      tag: {
        type: 'warn',
        dot: true,
      },
    }]
  },
};
export default admin;

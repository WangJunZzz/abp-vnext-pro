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
      },
    }, {
      path: 'abpRole',
      name: t('routes.admin.roleManagement'),
      tag: {
        type: 'warn',
      },
    }]
  },
};
export default admin;

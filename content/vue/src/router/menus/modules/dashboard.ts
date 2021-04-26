import type { MenuModule } from '/@/router/types';
import { t } from '/@/hooks/web/useI18n';

const menu: MenuModule = {
  orderNo: 0,
  menu: {
    name: t('routes.dashboard.analysis'),
    path: '/dashboard',
    tag: {
      dot: true,
      type: 'warn',
    }
  },
};
export default menu;

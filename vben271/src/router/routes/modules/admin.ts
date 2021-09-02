import type { AppRouteModule } from '/@/router/types';
import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';
const IFrame = () => import('/@/views/sys/iframe/FrameBlank.vue');
import { useUserStoreWithOut } from '/@/store/modules/user';
const userStore = useUserStoreWithOut();
const token = userStore.getToken;

const admin: AppRouteModule = {
  path: '/admin',
  name: 'Admin',
  component: LAYOUT,
  //redirect: '/admin/abpUser',
  meta: {
    orderNo: 20,
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
        icon: 'ant-design:skin-outlined',
      },
    },
    {
      path: 'abpRole',
      name: 'AbpRole',
      component: () => import('/@/views/admin/roles/AbpRole.vue'),
      meta: {
        title: t('routes.admin.roleManagement'),
        //policy: 'AbpIdentity.Roles.Query',
        icon: 'ant-design:lock-outlined',
      },
    },
    {
      path: 'hangfire',
      name: '后台任务',
      component: IFrame,
      meta: {
        frameSrc: import.meta.env.VITE_API_URL + '/hangfire?access_token=' + token,
        title: '后台任务',
        //policy: 'AbpIdentity.Hangfire.Dashboard',
        icon: 'ant-design:clock-circle-outlined',
      },
    },
  ],
};

export default admin;

import TabsView from '@/layouts/tabs/TabsView'
import BlankView from '@/layouts/BlankView'


// 路由配置
const options = {
  mode: 'history',
  routes: [
    {
      path: '/login',
      name: '登录页',
      component: () => import('@/pages/login')
    },
    {
      path: '*',
      name: '404',
      component: () => import('@/pages/exception/404'),
    },
    {
      path: '/403',
      name: '403',
      component: () => import('@/pages/exception/403'),
    },
    {
      path: '/oidc-callback',
      name: 'oidc-callback',
      component: () => import('@/pages/oidc/oidc-callback'),
    },
    {
      path: '/oidc-auth',
      name: 'oidc-auth',
      component: () => import('@/pages/oidc/oidc-auth'),
    },
    {
      path: '/oidc-silent-renew',
      name: 'oidc-silent-renew',
      component: () => import('@/pages/oidc/oidc-silent-renew'),
    },
    {
      path: '/',
      name: '首页',
      component: TabsView,
      redirect: '/login',
      children: [
        {
          path: 'Dashboard',
          name: '首页',
          meta: {
            icon: 'file-ppt',
            policy: '*'
          },
          component: () => import('@/pages/dashboard')
        },
        {
          path: '/Admin',
          name: '系统管理',
          meta: {
            icon: 'dashboard'
          },
          component: BlankView,
          children: [
            {
              path: 'User',
              name: '用户',
              meta: {
                icon: 'user',
                policy: "AbpIdentity.Users"

              },
              component: () => import('@/pages/admin/user')
            },
            {
              path: 'Role',
              name: '角色',
              meta: {
                icon: 'lock',
                policy: "AbpIdentity.Roles"
              },
              component: () => import('@/pages/admin/role')
            },
            // {
            //   path: 'Tenant',
            //   name: '租户',
            //   meta: {
            //     icon: 'skin',
            //     policy: "AbpTenantManagement.Tenants"
            //   },
            //   component: () => import('@/pages/admin/tenant')
            // },
            {
              path: 'Dic',
              name: '数据字典',
              meta: {
                icon: 'profile',
                policy: "Zzz.Dic"
              },
              component: () => import('@/pages/admin/dic')
            },
            {
              path: 'Settings',
              name: '设置',
              meta: {
                icon: 'profile',
                policy: "SettingUi.ShowSettingPage"
              },
              component: () => import('@/pages/admin/settings')
            }
          ]
        }
      ]
    }
  ]
}

export default options

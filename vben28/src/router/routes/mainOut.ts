/**
The routing of this file will not show the layout.
It is an independent new page.
the contents of the file still need to log in to access
 */
import type { AppRouteModule } from '/@/router/types';

// test
// http:ip:port/main-out
export const mainOutRoutes: AppRouteModule[] = [
  {
    path: '/oidcSignIn',
    name: 'OidcSignIn',
    component: () => import('/@/views/sys/login/OidcSignIn.vue'),
    meta: {
      title: 'Oidc',
      ignoreAuth: true,
    },
  },
  {
    path: '/oidcSignOut',
    name: 'OidcSignOut',
    component: () => import('/@/views/sys/login/OidcSignOut.vue'),
    meta: {
      title: 'Oidc',
      ignoreAuth: true,
    },
  },
  {
    path: '/githubSignIn',
    name: 'GithubOidcSign',
    component: () => import('/@/views/sys/login/GithubOidcSignIn.vue'),
    meta: {
      title: 'GithubOidcSign',
      ignoreAuth: true,
    },
  },
];

export const mainOutRouteNames = mainOutRoutes.map((item) => item.name);

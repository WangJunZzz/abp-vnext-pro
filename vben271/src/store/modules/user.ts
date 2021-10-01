import type { UserInfo } from '/#/store';
import type { ErrorMessageMode } from '/#/axios';
import { defineStore } from 'pinia';
import { store } from '/@/store';
import { RoleEnum } from '/@/enums/roleEnum';
import { PageEnum } from '/@/enums/pageEnum';
import {
  ROLES_KEY,
  TOKEN_KEY,
  USER_INFO_KEY,
  ABP_LOCALE_KEY,
  ABP_TETANT_KEY,
} from '/@/enums/cacheEnum';
import { getAuthCache, setAuthCache } from '/@/utils/auth';
import { GetUserInfoByUserIdModel, LoginParams } from '/@/api/sys/model/userModel';
import {
  doLogout,
  //getUserInfo,
  //loginApi,
  login,
  getAbpApplicationConfiguration,
  stsLogin,
} from '/@/api/sys/user';
import { useI18n } from '/@/hooks/web/useI18n';
import { useMessage } from '/@/hooks/web/useMessage';
import { router } from '/@/router';
import { usePermissionStore } from '/@/store/modules/permission';
import { RouteRecordRaw } from 'vue-router';
import { PAGE_NOT_FOUND_ROUTE } from '/@/router/routes/basic';
import { LoginInput, AccountServiceProxy } from '/@/services/ServiceProxies';
import jwt_decode from 'jwt-decode';
interface UserState {
  userInfo: Nullable<UserInfo>;
  token?: string;
  roleList: RoleEnum[];
  sessionTimeout?: boolean;
  lastUpdateTime: number;
  language: string;
  tenantId: string;
}

export const useUserStore = defineStore({
  id: 'app-user',
  state: (): UserState => ({
    // user info
    userInfo: null,
    // token
    token: undefined,
    // roleList
    roleList: [],
    // Whether the login expired
    sessionTimeout: false,
    // Last fetch time
    lastUpdateTime: 0,
    language: '',
    tenantId: '',
  }),
  getters: {
    getUserInfo(): UserInfo {
      return this.userInfo || getAuthCache<UserInfo>(USER_INFO_KEY) || {};
    },
    getToken(): string {
      return this.token || getAuthCache<string>(TOKEN_KEY);
    },
    getRoleList(): RoleEnum[] {
      return this.roleList.length > 0 ? this.roleList : getAuthCache<RoleEnum[]>(ROLES_KEY);
    },
    getSessionTimeout(): boolean {
      return !!this.sessionTimeout;
    },
    getLastUpdateTime(): number {
      return this.lastUpdateTime;
    },
    getLanguage(): string {
      return this.language || getAuthCache<string>(ABP_LOCALE_KEY);
    },
    getTenant(): string {
      return this.tenantId || getAuthCache<string>(ABP_TETANT_KEY);
    },
    checkUserLoginExpire(): boolean {
      try {
        const userStore = useUserStoreWithOut();
        const token = userStore.getToken;
        if (!token) return true;
        const decoded: any = jwt_decode(token);
        // 获取当前时间戳
        let currentTimeStamp = new Date().getTime() / 1000;
        if (currentTimeStamp >= decoded.exp) {
          return true;
        } else {
          return false;
        }
      } catch (error) {
        return true;
      }
    },
  },
  actions: {
    setToken(info: string | undefined) {
      this.token = info;
      setAuthCache(TOKEN_KEY, info);
    },
    setRoleList(roleList: RoleEnum[]) {
      this.roleList = roleList;
      setAuthCache(ROLES_KEY, roleList);
    },
    setUserInfo(info: UserInfo) {
      this.userInfo = info;
      this.lastUpdateTime = new Date().getTime();
      setAuthCache(USER_INFO_KEY, info);
    },
    setTenant(tenantId: string) {
      this.tenantId = tenantId;
      setAuthCache(ABP_TETANT_KEY, tenantId);
    },
    setSessionTimeout(flag: boolean) {
      this.sessionTimeout = flag;
    },
    resetState() {
      this.userInfo = null;
      this.token = '';
      this.roleList = [];
      this.sessionTimeout = false;
    },
    /**
     * @description: login
     */
    // async login(
    //   params: LoginParams & {
    //     goHome?: boolean;
    //     mode?: ErrorMessageMode;
    //   }
    // ): Promise<GetUserInfoModel | null> {
    //   try {
    //     const { goHome = true, mode, ...loginParams } = params;
    //     const data = await loginApi(loginParams, mode);
    //     const { token } = data;

    //     // save token
    //     this.setToken(token);
    //     // get user info
    //     const userInfo = await this.getUserInfoAction();

    //     const sessionTimeout = this.sessionTimeout;
    //     if (sessionTimeout) {
    //       this.setSessionTimeout(false);
    //     } else if (goHome) {
    //       const permissionStore = usePermissionStore();
    //       if (!permissionStore.isDynamicAddedRoute) {
    //         const routes = await permissionStore.buildRoutesAction();
    //         routes.forEach((route) => {
    //           router.addRoute(route as unknown as RouteRecordRaw);
    //         });
    //         router.addRoute(PAGE_NOT_FOUND_ROUTE as unknown as RouteRecordRaw);
    //         permissionStore.setDynamicAddedRoute(true);
    //       }
    //       await router.replace(userInfo.homePath || PageEnum.BASE_HOME);
    //     }
    //     return userInfo;
    //   } catch (error) {
    //     return Promise.reject(error);
    //   }
    // },
    async login(
      params: LoginParams & {
        goHome?: boolean;
        mode?: ErrorMessageMode;
      }
    ): Promise<GetUserInfoByUserIdModel | null> {
      try {
        const { goHome = true } = params;
        const request = new LoginInput();
        request.name = params.username;
        request.password = params.password;
        const data = await login(request);
        this.setToken(data.token as string);
        this.setUserInfo({
          userId: data.id as string,
          username: data.userName as string,
          realName: data.name as string,
          roles: data.roles as [],
          avatar: '',
        });
        await this.getAbpApplicationConfigurationAsync();
        goHome && (await router.replace(PageEnum.BASE_HOME));
        return null;
      } catch (error) {
        console.log(error);
        router.replace(PageEnum.BASE_LOGIN);
        return null;
      }
    },
    async getAbpApplicationConfigurationAsync() {
      const application = await getAbpApplicationConfiguration();
      const permissionStore = usePermissionStore();
      const grantPolicy = Object.keys(application.auth?.grantedPolicies as object);
      permissionStore.setPermCodeList(grantPolicy);
    },

    async stsLogin(token: string) {
      try {
        const data = await stsLogin(token);
        this.setToken(data.token as string);
        this.setUserInfo({
          userId: data.id as string,
          username: data.userName as string,
          realName: data.name as string,
          roles: data.roles as [],
          avatar: '',
        });
        await this.getAbpApplicationConfigurationAsync();
        await router.replace(PageEnum.BASE_HOME);
      } catch (error) {
        router.replace(PageEnum.BASE_LOGIN);
      }
    },

    /**
     * @description: logout
     */
    async logout(goLogin = false) {
      try {
        //await doLogout();
      } catch {
        console.log('注销Token失败');
      }
      this.setToken(undefined);
      this.setSessionTimeout(false);
      goLogin && router.push(PageEnum.BASE_LOGIN);
    },

    /**
     * @description: Confirm before logging out
     */
    confirmLoginOut() {
      const { createConfirm } = useMessage();
      const { t } = useI18n();
      createConfirm({
        iconType: 'warning',
        title: t('sys.app.logoutTip'),
        content: t('sys.app.logoutMessage'),
        onOk: async () => {
          await this.logout(true);
        },
      });
    },
  },
});

// Need to be used outside the setup
export function useUserStoreWithOut() {
  return useUserStore(store);
}

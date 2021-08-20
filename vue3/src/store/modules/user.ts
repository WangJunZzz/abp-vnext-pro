import type { UserInfo } from '/#/store';
import type { ErrorMessageMode } from '/@/utils/http/axios/types';

import { defineStore } from 'pinia';
import { store } from '/@/store';

import { RoleEnum } from '/@/enums/roleEnum';
import { PageEnum } from '/@/enums/pageEnum';
import { ROLES_KEY, TOKEN_KEY, USER_INFO_KEY, ABP_LOCALE_KEY } from '/@/enums/cacheEnum';

import { getAuthCache, setAuthCache } from '/@/utils/auth';
import { GetUserInfoByUserIdModel, LoginParams } from '/@/api/sys/model/userModel';
import { usePermissionStore } from './permission';
import { login, getAbpApplicationConfiguration } from '/@/api/sys/user';
import { LoginInput } from '/@/services/ServiceProxies';
import { useI18n } from '/@/hooks/web/useI18n';
import { useMessage } from '/@/hooks/web/useMessage';
import router from '/@/router';

interface UserState {
  userInfo: Nullable<UserInfo>;
  token?: string;
  roleList: RoleEnum[];
  sessionTimeout?: boolean;
  language: string;
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
    language: '',
    // Whether the login expired
    sessionTimeout: false,
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
    getLanguage(): string {
      return this.language || getAuthCache<string>(ABP_LOCALE_KEY);
    },
  },
  actions: {
    setToken(info: string) {
      this.token = info;
      setAuthCache(TOKEN_KEY, info);
    },
    setRoleList(roleList: RoleEnum[]) {
      this.roleList = roleList;
      setAuthCache(ROLES_KEY, roleList);
    },
    setUserInfo(info: UserInfo) {
      this.userInfo = info;
      setAuthCache(USER_INFO_KEY, info);
    },
    setLanguage(value: string) {
      this.language = value;
      setAuthCache(ABP_LOCALE_KEY, value);
    },
    resetState() {
      this.userInfo = null;
      this.token = '';
      this.roleList = [];
      this.language = '';
    },
    /**
     * @description: login
     */
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
        });
        await this.getAbpApplicationConfigurationAsync();
        goHome && (await router.replace(PageEnum.BASE_HOME));
        return null;
      } catch (error) {
        console.log(error);
        return null;
      }
    },
    async getAbpApplicationConfigurationAsync() {
      var application = await getAbpApplicationConfiguration();
      const permissionStore = usePermissionStore();
      const grantPolicy = Object.keys(application.auth?.grantedPolicies as object);
      permissionStore.setPermCodeList(grantPolicy);
    },

    /**
     * @description: logout
     */
    logout(goLogin = false) {
      this.resetState;
      const permissionStore = usePermissionStore();
      permissionStore.resetState;
      localStorage.clear();
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

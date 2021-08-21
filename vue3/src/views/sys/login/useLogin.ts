import { RuleObject } from 'ant-design-vue/lib/form/interface';
import { ref, computed, unref, Ref } from 'vue';
import { useI18n } from '/@/hooks/web/useI18n';
import Oidc from 'oidc-client';

import { useUserStoreWithOut } from '/@/store/modules/user';
import { AccountServiceProxy } from '/@/services/ServiceProxies';
import { getAbpApplicationConfiguration } from '/@/api/sys/user';
import { usePermissionStore } from '/@/store/modules/permission';
import router from '/@/router';
import { PageEnum } from '/@/enums/pageEnum';

export enum LoginStateEnum {
  LOGIN,
  REGISTER,
  RESET_PASSWORD,
  MOBILE,
  QR_CODE,
}

const currentState = ref(LoginStateEnum.LOGIN);

export function useLoginState() {
  function setLoginState(state: LoginStateEnum) {
    currentState.value = state;
  }

  const getLoginState = computed(() => currentState.value);

  function handleBackLogin() {
    setLoginState(LoginStateEnum.LOGIN);
  }

  return { setLoginState, getLoginState, handleBackLogin };
}

export function useFormValid<T extends Object = any>(formRef: Ref<any>) {
  async function validForm() {
    const form = unref(formRef);
    if (!form) return;
    const data = await form.validate();
    return data as T;
  }

  return { validForm };
}

export function useFormRules(formData?: Recordable) {
  const { t } = useI18n();

  const getAccountFormRule = computed(() => createRule(t('sys.login.accountPlaceholder')));
  const getPasswordFormRule = computed(() => createRule(t('sys.login.passwordPlaceholder')));
  const getSmsFormRule = computed(() => createRule(t('sys.login.smsPlaceholder')));
  const getMobileFormRule = computed(() => createRule(t('sys.login.mobilePlaceholder')));

  const validatePolicy = async (_: RuleObject, value: boolean) => {
    return !value ? Promise.reject(t('sys.login.policyPlaceholder')) : Promise.resolve();
  };

  const validateConfirmPassword = (password: string) => {
    return async (_: RuleObject, value: string) => {
      if (!value) {
        return Promise.reject(t('sys.login.passwordPlaceholder'));
      }
      if (value !== password) {
        return Promise.reject(t('sys.login.diffPwd'));
      }
      return Promise.resolve();
    };
  };

  const getFormRules = computed(() => {
    const accountFormRule = unref(getAccountFormRule);
    const passwordFormRule = unref(getPasswordFormRule);
    const smsFormRule = unref(getSmsFormRule);
    const mobileFormRule = unref(getMobileFormRule);

    const mobileRule = {
      sms: smsFormRule,
      mobile: mobileFormRule,
    };
    switch (unref(currentState)) {
      // register form rules
      case LoginStateEnum.REGISTER:
        return {
          account: accountFormRule,
          password: passwordFormRule,
          confirmPassword: [{ validator: validateConfirmPassword(formData?.password), trigger: 'change' }],
          policy: [{ validator: validatePolicy, trigger: 'change' }],
          ...mobileRule,
        };

      // reset password form rules
      case LoginStateEnum.RESET_PASSWORD:
        return {
          account: accountFormRule,
          ...mobileRule,
        };

      // mobile form rules
      case LoginStateEnum.MOBILE:
        return mobileRule;

      // login form rules
      default:
        return {
          account: accountFormRule,
          password: passwordFormRule,
        };
    }
  });
  return { getFormRules };
}

function createRule(message: string) {
  return [
    {
      required: true,
      message,
      trigger: 'change',
    },
  ];
}

/**
 * sts登陆
 */
export function useYHStsLogin() {
  const { protocol, hostname, port } = window.location;
  let currentHost = `${protocol}//${hostname}${port ? `:${port}` : ''}`;
  const settings: any = {
    authority: import.meta.env.VITE_AUTH_URL,
    client_id: 'CompanyName.ProjectName',
    redirect_uri: currentHost + '/sts/callback',
    post_logout_redirect_uri: import.meta.env.VITE_AUTH_URL,
    response_type: `id_token token`,
    scope: 'openid email profile',
    //silent_redirect_uri: currentHost + '/oidc-silent-renew',
    automaticSilentRenew: true, // If true oidc-client will try to renew your token when it is about to expire
    automaticSilentSignin: true, // If true vuex-oidc will try to silently signin unauthenticated users on public routes. Defaults to true
  };

  const mgr = new Oidc.UserManager(settings);
  mgr.signinRedirect();
}

export function getYhStsUserInfo(token: string) {
  // const _loginServiceProxy = new AccountServiceProxy();
  // const userStore = useUserStoreWithOut();
  // _loginServiceProxy
  //   .sts(token)
  //   .then((data) => {
  //     userStore.setUserInfo({
  //       userId: data.id as string,
  //       username: data.userName as string,
  //       realName: data.name as string,
  //       roles: data.roles as [],
  //     });
  //     userStore.setToken(data.token as string);
  //     getAbpApplicationConfiguration().then((res) => {
  //       const permissionStore = usePermissionStore();
  //       const grantPolicy = Object.keys(res.auth?.grantedPolicies as object);
  //       permissionStore.setPermCodeList(grantPolicy);
  //       router.replace(PageEnum.BASE_HOME);
  //     });
  //   })
  //   .catch((error) => {
  //     if (error.error.message.indexOf('锁定') >= 0) {
  //       router.replace(PageEnum.BASE_LOGIN);
  //     }
  //   });
}

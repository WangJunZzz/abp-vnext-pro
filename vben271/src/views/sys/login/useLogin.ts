import type { ValidationRule } from 'ant-design-vue/lib/form/Form';
import type { RuleObject } from 'ant-design-vue/lib/form/interface';
import { ref, computed, unref, Ref } from 'vue';
import { useI18n } from '/@/hooks/web/useI18n';
import Oidc from 'oidc-client';
import { useUserStore } from '/@/store/modules/user';
export enum LoginStateEnum {
  LOGIN,
  REGISTER,
  RESET_PASSWORD,
  MOBILE,
  QR_CODE,
  TENANT,
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
  const getTenantFormRule = computed(() => createRule(t('sys.login.tenantPlaceholder')));
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

  const getFormRules = computed((): { [k: string]: ValidationRule | ValidationRule[] } => {
    const accountFormRule = unref(getAccountFormRule);
    const passwordFormRule = unref(getPasswordFormRule);
    const smsFormRule = unref(getSmsFormRule);
    const mobileFormRule = unref(getMobileFormRule);
    const tenantFormRule = unref(getTenantFormRule);

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
          confirmPassword: [
            { validator: validateConfirmPassword(formData?.password), trigger: 'change' },
          ],
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
      case LoginStateEnum.TENANT:
        return {
          account: accountFormRule,
          password: passwordFormRule,
          tenant: tenantFormRule,
        };
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

function getOidcSettings() {
  const { protocol, hostname, port } = window.location;
  let currentHost = `${protocol}//${hostname}${port ? `:${port}` : ''}`;
  const settings: any = {
    authority: import.meta.env.VITE_AUTH_URL,
    client_id: 'Vue3',
    redirect_uri: currentHost + '/oidcSignIn',
    post_logout_redirect_uri: currentHost + '/oidcSignOut',
    response_type: `id_token token`,
    scope: 'openid email profile',
    //silent_redirect_uri: currentHost + '/oidc-silent-renew',
    automaticSilentRenew: true, // If true oidc-client will try to renew your token when it is about to expire
    automaticSilentSignin: true, // If true vuex-oidc will try to silently signin unauthenticated users on public routes. Defaults to true
  };
  return settings;
}

export function useOidcLogin() {
  const settings = getOidcSettings();
  const mgr = new Oidc.UserManager(settings);
  mgr.signinRedirect();
}

export async function useOidcLogout() {
  const settings = getOidcSettings();
  const mgr = new Oidc.UserManager(settings);
  const userStore = useUserStore();
  await mgr.signoutRedirect({ id_token_hint: userStore.userInfo?.idToken });
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

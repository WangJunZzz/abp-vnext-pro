import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { message } from 'ant-design-vue';
import { useUserStoreWithOut } from '/@/store/modules/user';
import { router } from '/@/router';
import { PageEnum } from '/@/enums/pageEnum';
import { useI18n } from '/@/hooks/web/useI18n';
import { Modal } from 'ant-design-vue';
import { useLocale } from '/@/locales/useLocale';
export class ServiceProxyBase {
  protected transformOptions(options: AxiosRequestConfig) {
    options.baseURL = import.meta.env.VITE_API_URL as string;
    const guard: boolean = this.urlGuard(options.url as string);
    const userStore = useUserStoreWithOut();
    const { token, language } = this.buildRequestMessage();
    if (!guard) {
      if (userStore.checkUserLoginExpire) {
        router.replace(PageEnum.BASE_LOGIN);
        return;
      }
      // 添加header
      options.headers = {
        'accept-language': language,
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + token,
        __tenant: userStore.tenantId,
      };
    } else {
      options.headers = {
        'Content-Type': 'application/json',
        __tenant: userStore.tenantId,
        'accept-language': language,
      };
    }

    return Promise.resolve(options);
  }
  protected transformResult(
    _url: string,
    response: AxiosResponse,
    processor: (response: AxiosResponse) => Promise<any>
  ): Promise<any> {
    const { t } = useI18n();

    if (response.status == 401 || response.status == 403 || response.status == 302) {
      message.error(t('common.authorityText'));
      router.replace(PageEnum.BASE_LOGIN);
    } else if (response.status == 400) {
      Modal.error({
        title: '验证失败',
        content: response.data.error.validationErrors[0].message,
      });
    } else if (response.status >= 500) {
      Modal.error({
        title: '请求异常',
        content: response.data.error.message,
      });
    }

    return processor(response);
  }

  //判决接口是否需要拦截
  private urlGuard(url: string): boolean {
    if (url == '/Tenants/find') {
      return true;
    }

    if (url.startsWith('/api/app/account')) {
      return true;
    }

    return false;
  }

  private buildRequestMessage(): any {
    const userStore = useUserStoreWithOut();
    const token = userStore.getToken;
    const { getLocale } = useLocale();
    const language = getLocale.value == 'en' ? getLocale.value : 'zh-Hans';
    return {
      token,
      language,
    };
  }
}

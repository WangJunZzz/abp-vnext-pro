import { AxiosRequestConfig, AxiosResponse } from 'axios'
import { message } from 'ant-design-vue';
import { useUserStoreWidthOut } from '/@/store/modules/user';
import router from '/@/router';
import { PageEnum } from '/@/enums/pageEnum';
import { useI18n } from '/@/hooks/web/useI18n';
import { Modal } from 'ant-design-vue';
export class ServiceProxyBase {

  protected transformOptions(options: AxiosRequestConfig) {

    options.baseURL = import.meta.env.VITE_API_URL as string;
    const guard: boolean = this.urlGuard(options.url as string);
    if (!guard) {
      const { token, language } = this.getHeaderInfo();
      // 添加header
      options.headers = { 'accept-language': language, 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token }
    }


    return Promise.resolve(options);
  }
  protected transformResult(_url: string, response: AxiosResponse, processor: (response: AxiosResponse) => Promise<any>): Promise<any> {
    if (response.status == 401 || response.status == 403 || response.status == 302) {
      const { t } = useI18n();
      message.error(t('common.authorityText'));
      router.replace(PageEnum.BASE_LOGIN)
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
    // 登录接口不需要拦截
    return url.indexOf('login') as number > 0
  }

  private getHeaderInfo(): any {

    const userStore = useUserStoreWidthOut();
    const token = userStore.getToken;
    const language = userStore.getLanguage == undefined ? 'zh-Hans' : userStore.getLanguage;
    return {
      token, language
    };
  }
}

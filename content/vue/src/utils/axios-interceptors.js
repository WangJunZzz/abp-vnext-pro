const reqCommon = {
  /**
   * 发送请求之前做些什么
   * @param config axios config
   * @param options 应用配置 包含: {router, i18n, store, message}
   * @returns {*}
   */
  onFulfilled(config, options) {
    const { store } = options
    config.headers['Authorization'] = 'Bearer ' + store.state.account.token;
    config.headers['accept-language'] = store.state.account.defalutLanguage;
    return config
  },
  /**
   * 请求出错时做点什么
   * @param error 错误对象
   * @param options 应用配置 包含: {router, i18n, store, message}
   * @returns {Promise<never>}
   */
  onRejected(error, options) {
    const { message, router, store } = options
    const { response } = error

    if (response.status === 401 || response.status === 403) {
      router.push("/login");
    } else {
      let msg = response.data.error.message
      if (store.state.account.showErrorDetail) {
        msg = response.data.error.details
      }
      message.error(msg)
    }
    return Promise.reject(error)
  }
}

export default {
  request: [reqCommon], // 请求拦截
  response: [reqCommon] // 响应拦截
}

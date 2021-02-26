
export default {
  state: {
    userName: '',
    userEmail: '',
    name: '',
    token: '',
    defalutLanguage: 'zh-Hans',// 默认语言设置
    languages: [],// abp 语言列表集合
    localization: [], // abp 本地化
    permissions: [], // 用户拥有的权限
    showErrorDetail: true,// 是否显示错误详细信息
  },
  mutations: {
    setUserInfo(state, payload) {
      state.userName = payload.userName;
      state.userEmail = payload.userEmail,
        state.token = payload.token
    },
    setDefalutLanguage(state, value) {
      state.defalutLanguage = value;
    },
    setLanguages(state, value) {
      state.languages = value;
    },
    setLocalization(state, value) {
      state.localization = value;
    },
    setPermissions(state, value) {
      state.permissions = value;
    },
    clear(state) {
      state.userName = ''
      state.userEmail = ''
      state.token = ''
      state.name = ''
      state.defalutLanguage = "zh-Hans",
        state.languages = []
      state.localization = []
      state.permissions = []
    }
  }
}

<template>
  <LoginFormTitle v-show="getShow" class="enter-x" />
  <Form class="p-4 enter-x" :model="formData" :rules="getFormRules" ref="formRef" v-show="getShow">
    <FormItem name="account" class="enter-x">
      <Input size="large" v-model:value="formData.account" :placeholder="t('sys.login.userName')" />
    </FormItem>
    <FormItem name="password" class="enter-x">
      <InputPassword
        size="large"
        visibilityToggle
        v-model:value="formData.password"
        :placeholder="t('sys.login.password')"
      />
    </FormItem>

    <FormItem class="enter-x">
      <Button type="primary" size="large" block @click="handleLogin" :loading="loading">
        {{ t('sys.login.loginButton') }}
      </Button>
      <!-- <Button size="large" class="mt-4 enter-x" block @click="handleRegister">
        {{ t('sys.login.registerButton') }}
      </Button> -->
    </FormItem>
    <ARow class="enter-x">
      <ACol :span="12"> </ACol>
      <ACol :span="12">
        <FormItem :style="{ 'text-align': 'right' }">
          <!-- No logic, you need to deal with it yourself -->
          <Button type="link" size="small"> 忘记密码请联系系统管理员 </Button>
        </FormItem>
      </ACol>
    </ARow>

    <Divider class="enter-x">{{ t('sys.login.otherSignIn') }}</Divider>

    <div class="flex justify-evenly enter-x" :class="`${prefixCls}-sign-in-way`">
      <a-button type="link" @click="useYHStsLogin">{{ t('sys.login.sts') }} </a-button>
    </div>
  </Form>
</template>
<script lang="ts">
import { defineComponent, reactive, ref, toRaw, unref, computed } from 'vue';

import { Checkbox, Form, Input, Row, Col, Button, Divider } from 'ant-design-vue';
import {
  GithubFilled,
  WechatFilled,
  AlipayCircleFilled,
  GoogleCircleFilled,
  TwitterCircleFilled,
} from '@ant-design/icons-vue';
import LoginFormTitle from './LoginFormTitle.vue';

import { useI18n } from '/@/hooks/web/useI18n';
import { useMessage } from '/@/hooks/web/useMessage';

import { useUserStore } from '/@/store/modules/user';
import {
  LoginStateEnum,
  useLoginState,
  useFormRules,
  useFormValid,
  useYHStsLogin,
} from './useLogin';
import { useDesign } from '/@/hooks/web/useDesign';
import { onKeyStroke } from '@vueuse/core';

export default defineComponent({
  name: 'LoginForm',
  components: {
    [Col.name]: Col,
    [Row.name]: Row,
    Checkbox,
    Button,
    Form,
    FormItem: Form.Item,
    Input,
    Divider,
    LoginFormTitle,
    InputPassword: Input.Password,
    GithubFilled,
    WechatFilled,
    AlipayCircleFilled,
    GoogleCircleFilled,
    TwitterCircleFilled,
  },
  setup() {
    const { t } = useI18n();
    const { notification } = useMessage();
    const { prefixCls } = useDesign('login');
    const userStore = useUserStore();

    const { setLoginState, getLoginState } = useLoginState();
    const { getFormRules } = useFormRules();

    const formRef = ref();
    const loading = ref(false);
    const rememberMe = ref(false);

    const formData = reactive({
      account: '',
      password: '',
    });

    const { validForm } = useFormValid(formRef);

    onKeyStroke('Enter', handleLogin);

    const getShow = computed(() => unref(getLoginState) === LoginStateEnum.LOGIN);

    async function handleLogin() {
      const data = await validForm();
      if (!data) return;
      try {
        loading.value = true;
        const userInfo = await userStore.login(
          toRaw({
            password: data.password,
            username: data.account,
          })
        );
        if (userInfo) {
          notification.success({
            message: t('sys.login.loginSuccessTitle'),
            description: `${t('sys.login.loginSuccessDesc')}: ${userInfo.realName}`,
            duration: 3,
          });
        }
      } finally {
        loading.value = false;
      }
    }

    return {
      t,
      prefixCls,
      formRef,
      formData,
      getFormRules,
      rememberMe,
      handleLogin,
      loading,
      setLoginState,
      LoginStateEnum,
      getShow,
      useYHStsLogin,
    };
  },
});
</script>

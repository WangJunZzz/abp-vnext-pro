<template>
  <template v-if="getShow">
    <LoginFormTitle class="enter-x" />
    <div class="enter-x min-w-64 min-h-64">
      <div class="qr_login" id="qr_login"></div>
      <Divider class="enter-x">{{ t('sys.login.scanSign') }}</Divider>
      <Button size="large" block class="mt-4 enter-x" @click="handleBackLogin">
        {{ t('sys.login.backSignIn') }}
      </Button>
    </div>
  </template>
</template>
<script lang="ts" setup>
  import { computed, unref } from 'vue';
  import LoginFormTitle from './LoginFormTitle.vue';
  import { Button, Divider } from 'ant-design-vue';
  import { QrCode } from '/@/components/Qrcode/index';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { useLoginState, LoginStateEnum } from './useLogin';

  const qrCodeUrl = 'https://vvbin.cn/next/login';

  const { t } = useI18n();
  const { handleBackLogin, getLoginState } = useLoginState();

  const getShow = computed(() => unref(getLoginState) === LoginStateEnum.WORKWECHAT_QR_CODE);

  // window.getQRCode({
  //           "id": "qr_login",
  //           "appid": window.settings.config.corp_id,
  //           "agentid": window.settings.config.agent_id,
  //           "redirect_uri": encodeURI('http://myapp.com:3000/app'),
  //           "state": "hellowecom",
  //           "href": "",
  //           "lang": "zh",
  //       });      
</script>

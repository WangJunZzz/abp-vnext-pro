<template>
  <!-- <template> -->
  <div v-show="getShow">
    <!-- <LoginFormTitle class="enter-x" /> -->
    <div class="enter-x min-w-64 min-h-64">
      <div class="qr_login" id="qr_login"></div>
      <Button size="large" block class="mt-4 enter-x" @click="handleBackLogin">
        {{ t('sys.login.backSignIn') }}
      </Button>
    </div>
  </div>

  <!-- </template> -->
</template>
<script lang="ts" setup>
  import { computed, unref, nextTick } from 'vue';
  import LoginFormTitle from './LoginFormTitle.vue';
  import { Button, Divider } from 'ant-design-vue';
  import { QrCode } from '/@/components/Qrcode/index';

  import { useI18n } from '/@/hooks/web/useI18n';
  import { useLoginState, LoginStateEnum } from './useLogin';

  const { t } = useI18n();
  const { handleBackLogin, getLoginState } = useLoginState();
  const getShow = computed(() => unref(getLoginState) === LoginStateEnum.WORKWECHAT_QR_CODE);
  nextTick(() => {
    var wwLogin = new WwLogin({
      id: 'qr_login',
      appid: 'wwd54b6d4fa4de4ac1',
      agentid: '1000003',
      redirect_uri: encodeURI('http://myapp.com:4200/login'),
      state: 'hellowecom',
      href: '',
      lang: 'zh',
    });
  });
</script>

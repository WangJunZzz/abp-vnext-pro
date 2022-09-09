<template>
  <!-- <template> -->
    <div v-show="getShow">
    <!-- <LoginFormTitle class="enter-x" /> -->
    <div class="enter-x min-w-64 min-h-64">
      <div class="qr_login" id="qr_login"></div>
      <Divider class="enter-x">{{ t('sys.login.scanSign') }}</Divider>
      <Button size="large" block class="mt-4 enter-x" @click="handleBackLogin">
        {{ t('sys.login.backSignIn') }}
      </Button>
    </div>
    </div>

  <!-- </template> -->
</template>
<script lang="ts" setup>
  import { computed, unref,nextTick } from 'vue';
  import LoginFormTitle from './LoginFormTitle.vue';
  import { Button, Divider } from 'ant-design-vue';
  import { QrCode } from '/@/components/Qrcode/index';
  import { useRouter } from 'vue-router';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { useLoginState, LoginStateEnum } from './useLogin';
 
  const qrCodeUrl = 'https://vvbin.cn/next/login';

  const { t } = useI18n();
  const { handleBackLogin, getLoginState } = useLoginState();

  const getShow = computed(() => unref(getLoginState) === LoginStateEnum.WORKWECHAT_QR_CODE);
  nextTick(()=>{
var wwLogin =new WwLogin({
    id: 'qr_login',
    appid: "wwd54b6d4fa4de4ac1",
    agentid: "1000003",
    redirect_uri: encodeURI('http://myapp.com:4200/redirect'),
    state: "hellowecom",
    href: "",
    lang: "zh",});      


})
  
     /**
      * 使用watch监控当前 router 的变化
      */
  //  const router = useRouter()
  //   watch(() => router.currentRoute.value.path,(toPath) => {
  //     debugger
  //       //要执行的方法
  //    },{immediate: true,deep: true})
</script>

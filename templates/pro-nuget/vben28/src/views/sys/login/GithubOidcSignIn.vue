<template>
  <Loading :loading="loading" :absolute="absolute" :tip="tip" />
</template>
<script lang="ts">
  import { defineComponent, reactive, toRefs } from 'vue';
  import { Loading } from '/@/components/Loading';
  import { useUserStore } from '/@/store/modules/user';
  import { useRouter } from 'vue-router';
  import { router } from '/@/router';
  import { PageEnum } from '/@/enums/pageEnum';
  import { message } from 'ant-design-vue';
  export default defineComponent({
    components: { Loading },
    setup() {
      const compState = reactive({
        absolute: false,
        loading: false,
        tip: '登录中',
      });
      const userStore = useUserStore();
      async function openLoading(absolute: boolean) {
        compState.absolute = absolute;
        compState.loading = true;
        try {
     
          const { currentRoute } = useRouter();
          const code = currentRoute.value.fullPath.split('=')[1].split('&')[0];

          if (code) {
            await userStore.githubLogin(code);
          }
        } catch {
          message.error('登录失败');
          router.replace(PageEnum.BASE_HOME);
        } finally {
          compState.loading = false;
        }
      }
      openLoading(true);

      return {
        ...toRefs(compState),
      };
    },
  });
</script>

<template>
  <Loading :loading="loading" :absolute="absolute" :tip="tip" />
</template>
<script lang="ts">
  import { defineComponent, reactive, toRefs } from 'vue';
  import { Loading } from '/@/components/Loading';
  import { useUserStore } from '/@/store/modules/user';
  import { router } from '/@/router';
  import { PageEnum } from '/@/enums/pageEnum';
  import { message } from 'ant-design-vue';
  export default defineComponent({
    components: { Loading },
    setup() {
      const compState = reactive({
        absolute: false,
        loading: false,
        tip: '退出登录中',
      });
      const userStore = useUserStore();
      async function openLoading(absolute: boolean) {
        compState.absolute = absolute;
        compState.loading = true;
        try {
          userStore.resetState();
        } catch {
          message.error('退出登录中失败');
        } finally {
          compState.loading = false;
          router.replace(PageEnum.BASE_LOGIN);
        }
      }
      openLoading(true);

      return {
        ...toRefs(compState),
      };
    },
  });
</script>

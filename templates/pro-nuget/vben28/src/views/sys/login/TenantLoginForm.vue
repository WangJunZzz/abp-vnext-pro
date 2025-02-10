<template>
  <template v-if="getShow">
    <LoginFormTitle class="enter-x" />
    <Form class="p-4 enter-x" :model="formData" :rules="getFormRules" ref="formRef">
      <FormItem name="tenant" class="enter-x">
        <Input
          size="large"
          visibilityToggle
          v-model:value="formData.tenant"
          :placeholder="t('sys.login.tenant')"
        />
      </FormItem>
      <FormItem name="account" class="enter-x">
        <Input
          size="large"
          v-model:value="formData.account"
          :placeholder="t('sys.login.userName')"
          class="fix-auto-fill"
        />
      </FormItem>
      <FormItem name="password" class="enter-x">
        <InputPassword
          size="large"
          v-model:value="formData.password"
          :placeholder="t('sys.login.password')"
          class="fix-auto-fill"
        />
      </FormItem>

      <FormItem class="enter-x">
        <Button type="primary" size="large" block @click="handleLogin" :loading="loading">
          {{ t('sys.login.loginButton') }}
        </Button>
        <Button size="large" block class="mt-4" @click="handleBackLogin">
          {{ t('sys.login.backSignIn') }}
        </Button>
      </FormItem>
    </Form>
  </template>
</template>
<script lang="ts" setup>
  import { reactive, ref, computed, unref, toRaw } from 'vue';
  import { Form, Input, Button, message } from 'ant-design-vue';
  import { CountdownInput } from '/@/components/CountDown';
  import LoginFormTitle from './LoginFormTitle.vue';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { useLoginState, useFormRules, useFormValid, LoginStateEnum } from './useLogin';
  const InputPassword = Input.Password;
  const FormItem = Form.Item;
  import { useMessage } from '/@/hooks/web/useMessage';
  const { t } = useI18n();
  const { handleBackLogin, getLoginState } = useLoginState();
  const { getFormRules } = useFormRules();
  import { FindTenantByNameInput, TenantsServiceProxy } from '/@/services/ServiceProxies';
  const formRef = ref();
  const loading = ref(false);
  import { useUserStore } from '/@/store/modules/user';
  const formData = reactive({
    tenant: '',
    account: '',
    password: '',
  });

  const { validForm } = useFormValid(formRef);
  const { notification, createErrorModal } = useMessage();
  const getShow = computed(() => unref(getLoginState) === LoginStateEnum.TENANT);
  const userStore = useUserStore();
  async function handleLogin() {
    const data = await validForm();
    if (!data) return;

    // 判断租户是否存在
    const _tenantsServiceProxy = new TenantsServiceProxy();
    let request = new FindTenantByNameInput();
    request.name = data.tenant;
    const tenantResponse = await _tenantsServiceProxy.find(request);
    if (!tenantResponse.success) {
      message.error('租户不存在');
      return;
    }

    try {
      userStore.setTenant(tenantResponse.tenantId as string);
      loading.value = true;
      const userInfo = await userStore.login(
        toRaw({
          password: data.password,
          username: data.account,
          tenantId: tenantResponse.tenantId as string,
          mode: 'none', //不要默认的错误提示
        })
      );
      if (userInfo) {
        notification.success({
          message: t('sys.login.loginSuccessTitle'),
          description: `${t('sys.login.loginSuccessDesc')}: ${userInfo.realName}`,
          duration: 3,
        });
      }
    } catch (error) {
      userStore.setTenant('');
    } finally {
      loading.value = false;
    }
  }
</script>

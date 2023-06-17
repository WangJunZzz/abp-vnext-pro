<template>
  <BasicModal
    :title="t('sys.login.forgetFormTitle')"
    v-bind="$attrs"
    @register="register"
    :canFullscreen="false"
    @ok="submit"
    :minHeight="100"
  >
    <BasicForm @register="registerForm" />
  </BasicModal>
</template>
<script lang="ts">
  import { defineComponent } from 'vue';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { BasicModal, useModalInner } from '/@/components/Modal/index';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { UsersServiceProxy, ChangePasswordInput } from '/@/services/ServiceProxies';
  import { message } from 'ant-design-vue';

  export default defineComponent({
    name: 'DefaultWarehouse',
    components: { BasicModal, BasicForm },

    setup() {
      const { t } = useI18n();
      //const userStore = useUserStore();

      const [register, { changeOkLoading, closeModal }] = useModalInner();

      const [registerForm, { getFieldsValue, validate, resetFields }] = useForm({
        showActionButtonGroup: false,
        schemas: [
          {
            field: 'currentPassword',
            label: t('routes.admin.currentPassword'),
            component: 'InputPassword',
            required: true,
            labelWidth: 110,
            colProps: {
              span: 22,
            },
          },
          {
            field: 'newPassword',
            label: t('routes.admin.newPassword'),
            component: 'InputPassword',
            required: true,
            labelWidth: 110,
            colProps: {
              span: 22,
            },
          },
          {
            field: 'confirmPassword',
            label: t('routes.admin.confirmPassword'),
            component: 'InputPassword',
            required: true,
            labelWidth: 110,
            colProps: {
              span: 22,
            },
          },
        ],
      });
      const submit = async () => {
        try {
          changeOkLoading(true);
          await validate();
          const request = getFieldsValue();
          if (request.newPassword != request.confirmPassword) {
            message.error(t('routes.admin.editPasswordMessage'));
            changeOkLoading(false);
            return;
          }
          const _userServiceProxy = new UsersServiceProxy();

          await _userServiceProxy.changePassword(request as ChangePasswordInput);
          changeOkLoading(false);
          closeModal();
          await resetFields();
        } catch (error) {
          changeOkLoading(false);
        }
      };
      return {
        t,
        register,
        registerForm,
        submit,
      };
    },
  });
</script>

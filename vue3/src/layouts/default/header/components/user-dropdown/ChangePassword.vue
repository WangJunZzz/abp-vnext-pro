<template>
  <BasicModal :title="t('layout.header.updatePassword')" v-bind="$attrs" @register="register" :canFullscreen="false" @ok="submit" :height="200">
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
            label: '当前密码',
            component: 'InputPassword',
            required: true,
            labelWidth: 110,
            colProps: {
              span: 22,
            },
          },
          {
            field: 'newPassword',
            label: '新密码',
            component: 'InputPassword',
            required: true,
            labelWidth: 110,
            colProps: {
              span: 22,
            },
          },
          {
            field: 'confirmPassword',
            label: '密码(再次确认)',
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
            message.error('输入的2次密码不一致');
            changeOkLoading(false);
            return;
          }
          const _userServiceProxy = new UsersServiceProxy();

          var result = await _userServiceProxy.changePassword(request as ChangePasswordInput);
          changeOkLoading(false);
          if (!result) {
            message.error('密码修改失败');
          } else {
            closeModal();
          }
          resetFields();
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

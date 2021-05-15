<template>
  <BasicModal
    :title="t('layout.header.changePassword')"
    v-bind="$attrs"
    @register="register"
    :canFullscreen="false"
    @ok="submit"
    :minHeight="120"
    :height="120"
  >
    <BasicForm @register="registerForm" />
  </BasicModal>
</template>
<script lang="ts">
  import { defineComponent } from 'vue';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { BasicModal, useModalInner } from '/@/components/Modal/index';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { UserServiceProxy, ChangePasswordInput } from '/@/services/ServiceProxies';
  import { message } from 'ant-design-vue';

  export default defineComponent({
    name: 'DefaultWarehouse',
    components: { BasicModal, BasicForm },

    setup() {
      const { t } = useI18n();

      const [register, { changeOkLoading, closeModal }] = useModalInner();

      const [registerForm, { getFieldsValue, validate, resetFields }] = useForm({
        showActionButtonGroup: false,
        schemas: [
          {
            field: 'currentPassword',
            label: t('layout.header.currentPassword'),
            component: 'InputPassword',
            required: true,
            labelWidth: 120,
            colProps: {
              span: 22,
            },
          },
          {
            field: 'newPassword',
            label: t('layout.header.newPassword'),
            component: 'InputPassword',
            required: true,
            labelWidth: 120,
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
          const _userServiceProxy = new UserServiceProxy();
          const request = getFieldsValue() as ChangePasswordInput;
          var result = await _userServiceProxy.changePassword(request);
          changeOkLoading(false);
          if (!result) {
            message.error(t('common.operationFail'));
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
<style lang="less" scoped></style>

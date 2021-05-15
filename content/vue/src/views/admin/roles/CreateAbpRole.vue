<template>
  <BasicModal
    :title="t('routes.admin.roleManagement_create_role')"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerModal"
  >
    <BasicForm @register="registerUserForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent, useContext, defineEmit } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { createFormSchema, createRoleAsync } from './AbpRole';
  import { useI18n } from '/@/hooks/web/useI18n';
  // import { IdentityUserCreateDto } from '/@/services/ServiceProxies';
  export default defineComponent({
    name: 'CreateAbpRole',
    components: {
      BasicModal,
      BasicForm,
    },
    setup() {
      // 加载父组件方法
      defineEmit(['reload']);
      const ctx = useContext();

      const { t } = useI18n();
      const [registerUserForm, { getFieldsValue, validate, resetFields }] = useForm({
        labelWidth: 120,
        schemas: createFormSchema,
        showActionButtonGroup: false,
      });

      const [registerModal, { changeOkLoading, closeModal }] = useModalInner();

      // 保存角色
      const submit = async () => {
        try {
          const request = getFieldsValue();
          await createRoleAsync({ request, changeOkLoading, validate, closeModal });
          resetFields();
          ctx.emit('reload');
        } catch (error) {
          changeOkLoading(false);
        }
      };

      const cancel = () => {
        closeModal();
      };
      return {
        t,
        registerModal,
        registerUserForm,
        submit,
        cancel,
      };
    },
  });
</script>

<style lang="less" scoped></style>

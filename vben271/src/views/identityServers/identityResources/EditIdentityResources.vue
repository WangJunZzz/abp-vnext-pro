<template>
  <BasicModal
    :title="t('common.editText')"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerModal"
  >
    <BasicForm @register="registerIdentityResourcesForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { editFormSchema, editIdentityResourceAsync } from './IdentityResources';
  import { useI18n } from '/@/hooks/web/useI18n';

  export default defineComponent({
    name: 'EditIdentityResources',
    components: {
      BasicModal,
      BasicForm,
    },
    emits: ['reload'],
    setup(_, { emit }) {
      const { t } = useI18n();
      const [
        registerIdentityResourcesForm,
        { getFieldsValue, validate, resetFields, setFieldsValue },
      ] = useForm({
        labelWidth: 120,
        schemas: editFormSchema,
        showActionButtonGroup: false,
      });

      const [registerModal, { changeOkLoading, closeModal }] = useModalInner((data) => {
        setFieldsValue({
          name: data.record.name,
          enabled: data.record.enabled,
          displayName: data.record.displayName,
          description: data.record.description,
          required: data.record.required,
          emphasize: data.record.emphasize,
          showInDiscoveryDocument: data.record.showInDiscoveryDocument,
        });
      });

      // 保存角色
      const submit = async () => {
        try {
          const request = getFieldsValue();
          await validate();
          await editIdentityResourceAsync({ request, changeOkLoading, closeModal });
          resetFields();
          emit('reload');
        } catch (error) {
          changeOkLoading(false);
        }
      };

      const cancel = () => {
        resetFields();
        closeModal();
      };
      return {
        t,
        registerModal,
        registerIdentityResourcesForm,
        submit,
        cancel,
      };
    },
  });
</script>

<style lang="less" scoped></style>

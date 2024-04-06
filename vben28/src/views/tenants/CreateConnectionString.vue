<template>
    <BasicModal :title="t('common.createOrUpdateText')" :canFullscreen="false" @ok="submit" @cancel="cancel"
        @register="registerModal">
        <BasicForm @register="registerTenantForm" />
    </BasicModal>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { BasicModal, useModalInner } from '/@/components/Modal';
import { BasicForm, useForm } from '/@/components/Form/index';
import { createConnectionStringFormSchema, addOrUpdateConnectionString } from '/@/views/tenants/Tenant';
import { useI18n } from '/@/hooks/web/useI18n';

export default defineComponent({
    name: 'CreateConnectionString',
    components: {
        BasicModal,
        BasicForm,
    },
    emits: ['reload', 'register'],
    setup(_, { emit }) {
        const { t } = useI18n();
        const [registerTenantForm, { getFieldsValue, setFieldsValue }] = useForm({
            labelWidth: 120,
            schemas: createConnectionStringFormSchema,
            showActionButtonGroup: false,
        });

        const [registerModal, { closeModal }] = useModalInner((data) => {
            setFieldsValue({
                id: data.id,
            })
        });

        const submit = async () => {
            const request = getFieldsValue();
            await addOrUpdateConnectionString({ request });
            setFieldsValue({
                name: '',
            })
            setFieldsValue({
                value: '',
            })
            emit('reload');
            closeModal();

        };

        const cancel = () => {
            setFieldsValue({
                name: '',
            })
            setFieldsValue({
                value: '',
            })
            closeModal();
        };
        return {
            t,
            registerModal,
            registerTenantForm,
            submit,
            cancel,
        };
    },
});
</script>

<style lang="less" scoped></style>
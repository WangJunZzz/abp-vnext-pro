<template>
  <BasicModal
    :title="t('common.editText')"
    :width="700"
    :canFullscreen="false"
    @ok="submit"
    @register="registerModal"
    :bodyStyle="{ 'padding-top': '0' }"
    :destroyOnClose="true"
  >
    <div>
      <Tabs>
        <TabPane tab="Basic" key="1">
          <BasicForm @register="registerDetailForm" />
        </TabPane>
        <TabPane tab="Options" key="2" forceRender>
          <BasicForm @register="registerOptionForm" />
        </TabPane>
        <TabPane tab="Tokens" key="3" forceRender>
          <BasicForm @register="registerTokenForm" />
        </TabPane>
        <TabPane tab="Secret" key="4" forceRender>
          <BasicForm @register="registerSecretForm" />
        </TabPane>
      </Tabs>
    </div>
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { Tabs } from 'ant-design-vue';
  import { useI18n } from '/@/hooks/web/useI18n';
  import {
    editBasicDetailSchema,
    editBasicOptionSchema,
    editBasicTokenSchema,
    editBasicSecretSchema,
    updateClientAsync,
  } from '/@/views/identityServers/clients/Clients';

  export default defineComponent({
    name: 'EditAbpUser',
    components: {
      BasicModal,
      BasicForm,
      Tabs,
      TabPane: Tabs.TabPane,
    },
    emits: ['reload', 'register'],
    setup(_, { emit }) {
      const { t } = useI18n();
      const [
        registerDetailForm,
        {
          getFieldsValue: getFieldsDetailValue,
          validate: detailValidate,
          setFieldsValue: setDetailFieldsValue,
        },
      ] = useForm({
        labelWidth: 120,
        schemas: editBasicDetailSchema,
        showActionButtonGroup: false,
      });
      const [
        registerOptionForm,
        {
          getFieldsValue: getFieldsOptionValue,
          validate: optionValidate,
          setFieldsValue: setOptionFieldsValue,
        },
      ] = useForm({
        labelWidth: 120,
        schemas: editBasicOptionSchema,
        showActionButtonGroup: false,
      });
      const [
        registerTokenForm,
        {
          getFieldsValue: getFieldsTokenValue,
          validate: tokenValidate,
          setFieldsValue: setTokenFieldsValue,
        },
      ] = useForm({
        labelWidth: 120,
        schemas: editBasicTokenSchema,
        showActionButtonGroup: false,
      });
      const [
        registerSecretForm,
        {
          getFieldsValue: getFieldsSecretValue,
          validate: secretValidate,
          setFieldsValue: setSecretFieldsValue,
        },
      ] = useForm({
        labelWidth: 120,
        schemas: editBasicSecretSchema,
        showActionButtonGroup: false,
      });

      const [registerModal, { changeOkLoading, closeModal }] = useModalInner((data) => {
        setDetailFieldsValue({
          clientId: data.record.clientId,
          clientName: data.record.clientName,
          description: data.record.description,
          allowedGrantTypes: data.record.allowedGrantTypes[0].grantType,
          clientUri: data.record.clientUri,
          logoUri: data.record.logoUri,
          frontChannelLogoutUri: data.record.frontChannelLogoutUri,
          backChannelLogoutUri: data.record.backChannelLogoutUri,
        });
        setOptionFieldsValue({
          enabled: data.record.enabled,
          requireClientSecret: data.record.requireClientSecret,
          requireConsent: data.record.requireConsent,
          allowRememberConsent: data.record.allowRememberConsent,
          requirePkce: data.record.requirePkce,
          allowOfflineAccess: data.record.allowOfflineAccess,
          enableLocalLogin: data.record.enableLocalLogin,
          includeJwtId: data.record.includeJwtId,
          allowAccessTokensViaBrowser: data.record.allowAccessTokensViaBrowser,
          alwaysIncludeUserClaimsInIdToken: data.record.alwaysIncludeUserClaimsInIdToken,
          frontChannelLogoutSessionRequired: data.record.frontChannelLogoutSessionRequired,
          backChannelLogoutSessionRequired: data.record.backChannelLogoutSessionRequired,
        });
        setTokenFieldsValue({
          accessTokenLifetime: data.record.accessTokenLifetime,
          identityTokenLifetime: data.record.identityTokenLifetime,
          authorizationCodeLifetime: data.record.authorizationCodeLifetime,
          absoluteRefreshTokenLifetime: data.record.absoluteRefreshTokenLifetime,
          slidingRefreshTokenLifetime: data.record.slidingRefreshTokenLifetime,
          refreshTokenExpiration: data.record.refreshTokenExpiration,
          deviceCodeLifetime: data.record.deviceCodeLifetime,
        });

        if (data.record.clientSecrets.length > 0) {
          setSecretFieldsValue({
            secretType: data.record.clientSecrets[0].type,
            secret: data.record.clientSecrets[0].value,
          });
        }
      });

      const submit = async () => {
        await detailValidate();
        await optionValidate();
        await tokenValidate();
        await secretValidate();
        const detailRequest = getFieldsDetailValue();
        const optionRequest = getFieldsOptionValue();
        const tokenRequest = getFieldsTokenValue();
        const secretRequest = getFieldsSecretValue();
        const request = Object.assign(detailRequest, optionRequest, tokenRequest, secretRequest);
        await updateClientAsync({ request, changeOkLoading, closeModal });
        emit('reload');
      };
      return {
        registerModal,
        registerDetailForm,
        registerOptionForm,
        registerTokenForm,
        registerSecretForm,
        submit,
        t,
      };
    },
  });
</script>
<style lang="less" scoped>
  .ant-checkbox-wrapper + .ant-checkbox-wrapper {
    margin-left: 0;
  }
</style>

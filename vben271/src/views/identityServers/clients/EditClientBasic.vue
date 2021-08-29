<template>
  <BasicModal
    title="编辑Client"
    :width="700"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerModal"
    @visible-change="visibleChange"
    :bodyStyle="{ 'padding-top': '0' }"
    :destroyOnClose="true"
    :maskClosable="false"
  >
    <div>
      <Tabs>
        <TabPane tab="基本信息" key="1">
          <BasicForm @register="registerDetailForm" />
        </TabPane>
        <TabPane tab="Options" key="2">
          <BasicForm @register="registerOptionForm" />
        </TabPane>
        <TabPane tab="Tokens" key="3">
          <BasicForm @register="registerTokenForm" />
        </TabPane>
        <TabPane tab="Secret" key="4">
          <BasicForm @register="registerSecretForm" />
        </TabPane>
      </Tabs>
    </div>
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent, defineEmit } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { Tabs } from 'ant-design-vue';
  import { editBasicDetailSchema, editBasicOptionSchema, editBasicTokenSchema, editBasicSecretSchema } from './Clients';

  export default defineComponent({
    name: 'EditAbpUser',
    components: {
      BasicModal,
      BasicForm,
      Tabs,
      TabPane: Tabs.TabPane,
    },
    setup() {
      // 加载父组件方法
      // defineEmit(['reload']);

      const [registerDetailForm, { getFieldsValue: getFieldsDetailValue, validate: detailValidate, setFieldsValue: setDetailFieldsValue }] = useForm({
        labelWidth: 120,
        schemas: editBasicDetailSchema,
        showActionButtonGroup: false,
      });
      const [registerOptionForm, { getFieldsValue: getFieldsOptionValue, validate: optionValidate, setFieldsValue: setOptionFieldsValue }] = useForm({
        labelWidth: 120,
        schemas: editBasicOptionSchema,
        showActionButtonGroup: false,
      });
      const [registerTokenForm, { getFieldsValue: getFieldsTokenValue, validate: tokenValidate, setFieldsValue: setTokenFieldsValue }] = useForm({
        labelWidth: 120,
        schemas: editBasicTokenSchema,
        showActionButtonGroup: false,
      });
      const [registerSecretForm, { getFieldsValue: getFieldsSecretnValue, validate: secretValidate, setFieldsValue: setSecretFieldsValue }] = useForm(
        {
          labelWidth: 120,
          schemas: editBasicSecretSchema,
          showActionButtonGroup: false,
        }
      );
      let currentClient: any;
      const [registerModal, { changeOkLoading, closeModal }] = useModalInner((data) => {
        currentClient = data.record;
        console.log(currentClient);

        setDetailFieldsValue({
          clientId: data.record.clientId,
          clientName: data.record.clientName,
          description: data.record.description,
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

      const visibleChange = async (visible: boolean) => {
        if (visible) {
          detailValidate();
          optionValidate();
          tokenValidate();
          secretValidate();
          getFieldsTokenValue();
          getFieldsSecretnValue();
          getFieldsOptionValue();
          getFieldsDetailValue();
          changeOkLoading(true);
        } else {
        }
      };

      const submit = async () => {};
      const cancel = () => {
        closeModal();
      };

      return {
        registerModal,
        registerDetailForm,
        registerOptionForm,
        registerTokenForm,
        registerSecretForm,
        submit,
        visibleChange,
        cancel,
      };
    },
  });
</script>
<style lang="less" scoped>
  .ant-checkbox-wrapper + .ant-checkbox-wrapper {
    margin-left: 0px;
  }
</style>

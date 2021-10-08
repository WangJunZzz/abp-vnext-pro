<template>
  <BasicDrawer v-bind="$attrs" @register="register" title="Uri" width="30%">
    <Tabs>
      <TabPane tab="RedirectUri" key="1">
        <a-input
          placeholder="Please Enter Uri"
          style="width: 470px"
          v-model:value="redirectUriValue"
        />
        <a-button type="primary" @click="handleAddRedirectUri" style="margin-bottom: 10px">{{
          t('common.createText')
        }}</a-button>

        <div v-for="item in currentClient.redirectUris" :key="item.redirectUri">
          <a-input placeholder="" style="width: 470px" :defaultValue="item.redirectUri" disabled />
          <a-button type="primary" @click="handleRemoveRedirectUri(item.redirectUri)" danger>{{
            t('common.delText')
          }}</a-button>
        </div>
      </TabPane>
      <TabPane tab="LogoutRedirectUri" key="2" forceRender>
        <a-input
          placeholder="Please Enter Uri"
          style="width: 470px"
          v-model:value="postLogoutRedirectUriValue"
        />
        <a-button type="primary" @click="handleAddLogoutRedirectUri" style="margin-bottom: 10px">{{
          t('common.createText')
        }}</a-button>
        <div v-for="item in currentClient.postLogoutRedirectUris" :key="item.postLogoutRedirectUri">
          <a-input
            placeholder=""
            style="width: 470px"
            :defaultValue="item.postLogoutRedirectUri"
            disabled
          />
          <a-button
            type="primary"
            @click="handleRemoveLogoutRedirectUri(item.postLogoutRedirectUris)"
            danger
            >{{ t('common.delText') }}</a-button
          >
        </div>
      </TabPane>
      <TabPane tab="CORS" key="3" forceRender>
        <a-input placeholder="Please Enter Uri" style="width: 470px" v-model:value="originValue" />
        <a-button type="primary" @click="handleAddCors" style="margin-bottom: 10px">{{
          t('common.createText')
        }}</a-button>
        <div v-for="item in currentClient.allowedCorsOrigins" :key="item.origin">
          <a-input placeholder="" style="width: 470px" :defaultValue="item.origin" disabled />
          <a-button type="primary" @click="handleRemoveCors(item.origin)" danger>{{
            t('common.delText')
          }}</a-button>
        </div>
      </TabPane>
    </Tabs>
  </BasicDrawer>
</template>
<script lang="ts">
  import { defineComponent, reactive, toRefs } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { Tabs } from 'ant-design-vue';
  import { useI18n } from '/@/hooks/web/useI18n';
  import {
    addRedirectUriAsync,
    removeRedirectUriAsync,
    addLogoutRedirectUriAsync,
    removeLogoutRedirectUriAsync,
    addCorsAsync,
    removeCorsAsync,
  } from './Clients';
  import { PagingClientListOutput } from '/@/services/ServiceProxies';
  export default defineComponent({
    name: 'ClientUri',
    components: { BasicDrawer, Tabs, TabPane: Tabs.TabPane },
    emits: ['reload'],
    setup(_, { emit }) {
      const { t } = useI18n();
      let currentClient: PagingClientListOutput = new PagingClientListOutput();

      const state = reactive({
        redirectUriValue: '',
        postLogoutRedirectUriValue: '',
        originValue: '',
        currentClient,
      });
      const [register, { closeDrawer }] = useDrawerInner((data) => {
        state.currentClient = data.record;
      });

      const handleAddRedirectUri = async () => {
        if (state.redirectUriValue) {
          await addRedirectUriAsync({
            clientId: state.currentClient.clientId,
            uri: state.redirectUriValue,
          });
          state.redirectUriValue = '';
          emit('reload');
          closeDrawer();
        }
      };

      const handleRemoveRedirectUri = async (redirectUri: string) => {
        await removeRedirectUriAsync({
          clientId: state.currentClient.clientId,
          uri: redirectUri,
        });

        closeDrawer();
        emit('reload');
      };

      const handleAddLogoutRedirectUri = async () => {
        await addLogoutRedirectUriAsync({
          clientId: state.currentClient.clientId,
          uri: state.postLogoutRedirectUriValue,
        });
        state.postLogoutRedirectUriValue = '';
        closeDrawer();
        emit('reload');
      };

      const handleRemoveLogoutRedirectUri = async (redirectUri: string) => {
        await removeLogoutRedirectUriAsync({
          clientId: state.currentClient.clientId,
          uri: redirectUri,
        });

        closeDrawer();
        emit('reload');
      };

      const handleAddCors = async () => {
        await addCorsAsync({
          clientId: state.currentClient.clientId,
          origin: state.originValue,
        });
        closeDrawer();
        state.originValue = '';
        emit('reload');
      };

      const handleRemoveCors = async (origin: string) => {
        await removeCorsAsync({
          clientId: state.currentClient.clientId,
          origin: origin,
        });

        closeDrawer();
        emit('reload');
      };
      return {
        register,
        handleAddRedirectUri,
        handleRemoveRedirectUri,
        handleAddLogoutRedirectUri,
        handleRemoveLogoutRedirectUri,
        handleAddCors,
        handleRemoveCors,
        ...toRefs(state),
        t,
      };
    },
  });
</script>

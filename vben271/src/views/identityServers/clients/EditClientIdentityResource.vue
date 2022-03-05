<template>
  <BasicDrawer v-bind="$attrs" @register="register" title="Scopes" width="26%">
    <a-checkbox-group v-model:value="defaultScope">
      <a-row justify="center">
        <a-col :span="24">
          <a-checkbox
            style="width: 200px"
            v-for="(item, index) in scopes"
            :key="index"
            :value="item.value"
            >{{ item.label }}</a-checkbox
          >
        </a-col>
      </a-row>
    </a-checkbox-group>
    <div
      :style="{
        position: 'absolute',
        right: 0,
        bottom: 0,
        width: '100%',
        borderTop: '1px solid #e9e9e9',
        padding: '10px 16px',
        background: '#fff',
        textAlign: 'right',
        zIndex: 1,
      }"
    >
      <a-button :style="{ marginRight: '8px' }" @click="closeDrawer"
        >{{ t('common.cancelText') }}
      </a-button>
      <a-button type="primary" @click="submit">
        {{ t('common.saveText') }}
      </a-button>
    </div></BasicDrawer
  >
</template>
<script lang="ts">
  import { defineComponent, reactive, toRefs } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { getAllScopeAsync, updateScopesAsync } from "/@/views/identityServers/clients/Clients";
  import { StringStringFromSelector } from '/@/services/ServiceProxies';
  import { useI18n } from '/@/hooks/web/useI18n';
  export default defineComponent({
    name: 'EditClientIdentityResource',
    components: { BasicDrawer },
    emits: ['reload'],
    setup(_, { emit }) {
      const { t } = useI18n();
      let scopes: StringStringFromSelector[] = [];
      const state = reactive({
        defaultScope: [],
        scopes,
      });
      let clientId;
      const [register, { closeDrawer }] = useDrawerInner(async (data) => {
        console.log(data);
        clientId = data.record.clientId;
        state.scopes = await getAllScopeAsync();
        state.defaultScope = data.record.allowedScopes.map((e) => e.scope);
      });

      const submit = async () => {
        await updateScopesAsync({ clientId, scopes: state.defaultScope });
        closeDrawer();
        emit('reload');
      };
      return {
        register,
        closeDrawer,
        submit,
        ...toRefs(state),
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

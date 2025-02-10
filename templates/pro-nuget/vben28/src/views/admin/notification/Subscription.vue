<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button
          preIcon="ant-design:plus-circle-outlined"
          type="primary"
          @click="openCreateNotificationModal"
        >
      {{t('routes.admin.notificationManagement_sendNotificationSubscription')}}
        </a-button>
      </template>
      <template #bodyCell="{ column, record }">
  
        <template v-if="column.key === 'messageLevelName'">
          <Tag color='yellow' v-if="record.messageLevel ===10">{{ record.messageLevelName}}</Tag>
          <Tag color='green' v-if="record.messageLevel === 20">{{ record.messageLevelName}}</Tag>
          <Tag color='red' v-if="record.messageLevel === 30">{{ record.messageLevelName}}</Tag>
        </template>
        
      </template>

    </BasicTable>
    <CreateSubscription
      @register="registerCreateNotificationModal"
      @reload="reload"
      :bodyStyle="{ 'padding-top': '0' }"
    />
  </div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicTable, TableAction, useTable } from '/@/components/Table';
  import { Tag } from 'ant-design-vue';
  import { tableSubscriptionColumns, searchSubscriptionFormSchema, notificationPageAsync } from './Index';
  import { useI18n } from '/@/hooks/web/useI18n';
  import CreateSubscription from './CreateSubscription.vue';
  import { useModal } from '/@/components/Modal';
  export default defineComponent({
    name: 'Subscription',
    components: {
      BasicTable,
      TableAction,
      Tag,
      CreateSubscription
    },
    setup() {
      const { t } = useI18n();
      // table配置
      const [registerTable, { reload }] = useTable({
        columns: tableSubscriptionColumns,
        formConfig: {
          labelWidth: 70,
          schemas: searchSubscriptionFormSchema,
        },
        api: notificationPageAsync,
        showTableSetting: true,
        useSearchForm: true,
        bordered: true,
        canResize: true,
        showIndexColumn: true,
        immediate: true,
        scroll: { x: true },
      });
      const [registerCreateNotificationModal, { openModal: openCreateNotificationModal }] = useModal();
      return {
        registerTable,
        reload,
        t,
        registerCreateNotificationModal,
        openCreateNotificationModal
      };
    },
  });
</script>

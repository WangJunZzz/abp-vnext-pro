<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-button
          preIcon="ant-design:plus-circle-outlined"
          type="primary"
          @click="openCreateNotificationModal"
        >
      {{t('routes.admin.notificationManagement_sendNotification')}}
        </a-button>
      </template>
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'read'">
          <Tag color='green' v-if="record.read">{{ record.read? '已读' : '未读' }}</Tag>
          <Tag  v-if="!record.read">{{ record.read? '已读' : '未读' }}</Tag>
        </template>

        <template v-if="column.key === 'messageLevelName'">
          <Tag color='yellow' v-if="record.messageLevel ===10">{{ record.messageLevelName}}</Tag>
          <Tag color='green' v-if="record.messageLevel === 20">{{ record.messageLevelName}}</Tag>
          <Tag color='red' v-if="record.messageLevel === 30">{{ record.messageLevelName}}</Tag>
        </template>
        
      </template>

      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:note-edit-line',
              label: t('routes.admin.notificationManagement_setRead'),   
              onClick: handleSetRead.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>
    <CreateNotification
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
  import { tableColumns, searchFormSchema, notificationPageAsync,setReadAsync } from './Index';
  import { useI18n } from '/@/hooks/web/useI18n';
  import {  SetReadInput } from '/@/services/ServiceProxies';
  import CreateNotification from './CreateNotification.vue';
  import { useModal } from '/@/components/Modal';
  export default defineComponent({
    name: 'Notification',
    components: {
      BasicTable,
      TableAction,
      Tag,
      CreateNotification
    },
    setup() {
      const { t } = useI18n();
      // table配置
      const [registerTable, { reload }] = useTable({
        columns: tableColumns,
        formConfig: {
          labelWidth: 70,
          schemas: searchFormSchema,
        },
        api: notificationPageAsync,
        showTableSetting: true,
        useSearchForm: true,
        bordered: true,
        canResize: true,
        showIndexColumn: true,
        immediate: true,
        scroll: { x: true },
        actionColumn: {
          width: 220,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        },
      });
      const [registerCreateNotificationModal, { openModal: openCreateNotificationModal }] = useModal();
    async function handleSetRead(record: Recordable) {
          let request = new SetReadInput();
          request.id = record.id;
          await setReadAsync(request);
          await reload();
      }

   
      return {
        registerTable,
        reload,
        t,
        handleSetRead,
        registerCreateNotificationModal,
        openCreateNotificationModal
      };
    },
  });
</script>

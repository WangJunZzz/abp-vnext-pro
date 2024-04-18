import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import {
  NotificationServiceProxy,
  PagingNotificationSubscriptionInput,
  PagingNotificationInput,
  PagingUserListInput,
  UsersServiceProxy
} from '/@/services/ServiceProxies';
import { useI18n } from '/@/hooks/web/useI18n';
import { formatToDateTime, dateUtil } from '/@/utils/dateUtil';
const { t } = useI18n();
// 分页表格消息通知 BasicColumn
export const tableColumns: BasicColumn[] = [
  {
    title: t('routes.admin.notificationManagement_title'),
    dataIndex: 'title',
    width: 200,
  },
  {
    title: t('routes.admin.notificationManagement_content'),
    dataIndex: 'content',
  },
  // {
  //   title: t('routes.admin.notificationManagement_messageType'),
  //   dataIndex: 'messageTypeName',
  //   width: 100,
  // },
  {
    title: t('routes.admin.notificationManagement_messageLevel'),
    dataIndex: 'messageLevelName',
    width: 100,
  },

  {
    title: t('routes.admin.notificationManagement_senderUserName'),
    dataIndex: 'senderUserName',
    width: 100,
  },

  {
    title: t('routes.admin.notificationManagement_receiveUserName'),
    dataIndex: 'receiveUserName',
    width: 100,
  },
  {
    title: t('routes.admin.notificationManagement_read'),
    dataIndex: 'read',
    width: 100,
  },
  {
    title: t('routes.admin.notificationManagement_readTime'),
    dataIndex: 'readTime',
    customRender: ({ text }) => {
      return formatToDateTime(text);
    },
    width: 250,
  },
];

// 分页查询消息通知 FormSchema
export const searchFormSchema: FormSchema[] = [
  {
    field: 'title',
    label: t('routes.admin.notificationManagement_title'),
    component: "Input",
    colProps: { span: 4 }
  },
  {
    field: 'content',
    label: t('routes.admin.notificationManagement_content'),
    component: "Input",
    colProps: { span: 6 }
  },
  {
    field: 'messageType',
    label: t('routes.admin.notificationManagement_messageType'),
    component: "Input",
    required: true,
    defaultValue: 20,
    show: false,
    colProps: { span: 18 }
  },
  {
    field: 'messageLevel',
    component: 'Select',
    label: t('routes.admin.notificationManagement_messageLevel'),

    colProps: {
      span: 2,
    },
    componentProps: {
      options: [
        {
          label: '警告',
          value: 10,
        },
        {
          label: '正常',
          value: 20,
        },
        {
          label: '错误',
          value: 30,
        },
      ],
    },
  },
  {
    field: 'read',
    component: 'Select',
    label: t('routes.admin.notificationManagement_read'),

    colProps: {
      span: 2,
    },
    componentProps: {
      options: [
        {
          label: '是',
          value: true,
        },
        {
          label: '否',
          value: false,
        }
      ],
    },
  },

];

// 创建消息通知 FormSchema
export const createFormSchema: FormSchema[] = [
  {
    field: 'title',
    label: t('routes.admin.notificationManagement_title'),
    component: "Input",
    required: true,
    colProps: { span: 18 }
  },
  {
    field: 'content',
    label: t('routes.admin.notificationManagement_content'),
    component: "Input",
    required: true,
    colProps: { span: 18 }
  },
  {
    field: 'messageLevel',
    component: 'Select',
    label: t('routes.admin.notificationManagement_messageLevel'),
    defaultValue: 20,
    colProps: { span: 18 },
    componentProps: {
      options: [
        {
          label: '警告',
          value: 10,
        },
        {
          label: '正常',
          value: 20,
        },
        {
          label: '错误',
          value: 30,
        },
      ],
    },
  },
  {
    field: 'receiveUserId',
    label: t('routes.admin.notificationManagement_receiveUserName'),
    labelWidth: 120,
    component: 'ApiSelect',
    colProps: { span: 18 },
    componentProps: ({ formModel }) => {
      return {
        api: getUserListAsync,
        labelField: 'userName',
        valueField: 'id',
        showSearch: true,
        optionFilterProp: 'label',
        onChange: async (e: any, options : any) => {
          formModel.receiveUserName  = options.name;
        },
      };
    },
  },
  {
    field: 'receiveUserName',
    label: 'ReceiveUserName',
    component: "Input",
    required: true,
    show: false,
    colProps: { span: 18 }
  },
];

// 创建消息通知 FormSchema
export const createSubscriptionFormSchema: FormSchema[] = [
  {
    field: 'title',
    label: t('routes.admin.notificationManagement_title'),
    component: "Input",
    required: true,
    colProps: { span: 18 }
  },
  {
    field: 'content',
    label: t('routes.admin.notificationManagement_content'),
    component: "Input",
    required: true,
    colProps: { span: 18 }
  },
  {
    field: 'messageLevel',
    component: 'Select',
    label: t('routes.admin.notificationManagement_messageLevel'),
    defaultValue: 20,
    colProps: { span: 18 },
    componentProps: {
      options: [
        {
          label: '警告',
          value: 10,
        },
        {
          label: '正常',
          value: 20,
        },
        {
          label: '错误',
          value: 30,
        },
      ],
    },
  }
];


// 分页查询消息通知 FormSchema
export const searchSubscriptionFormSchema: FormSchema[] = [
  {
    field: 'title',
    label: t('routes.admin.notificationManagement_title'),
    component: "Input",
    colProps: { span: 4 }
  },
  {
    field: 'content',
    label: t('routes.admin.notificationManagement_content'),
    component: "Input",
    colProps: { span: 6 }
  },
  {
    field: 'messageType',
    label: t('routes.admin.notificationManagement_messageType'),
    component: "Input",
    required: true,
    defaultValue: 10,
    show: false,
    colProps: { span: 18 }
  },
  {
    field: 'messageLevel',
    component: 'Select',
    label: t('routes.admin.notificationManagement_messageLevel'),

    colProps: {
      span: 2,
    },
    componentProps: {
      options: [
        {
          label: '警告',
          value: 10,
        },
        {
          label: '正常',
          value: 20,
        },
        {
          label: '错误',
          value: 30,
        },
      ],
    },
  },
];
// 分页表格消息通知 BasicColumn
export const tableSubscriptionColumns: BasicColumn[] = [
  {
    title: t('routes.admin.notificationManagement_title'),
    dataIndex: 'title',
    width: 200,
  },
  {
    title: t('routes.admin.notificationManagement_content'),
    dataIndex: 'content',
  },
  // {
  //   title: t('routes.admin.notificationManagement_messageType'),
  //   dataIndex: 'messageTypeName',
  //   width: 100,
  // },
  {
    title: t('routes.admin.notificationManagement_messageLevel'),
    dataIndex: 'messageLevelName',
    width: 100,
  },

  {
    title: t('routes.admin.notificationManagement_senderUserName'),
    dataIndex: 'senderUserName',
    width: 100,
  },
  {
    title: t('routes.admin.userManagement_createTime'),
    dataIndex: 'creationTime',
    customRender: ({ text }) => {
      return dateUtil(text).format('YYYY-MM-DD HH:mm:ss');
    },
    width: 250,
  },
];

/**
 * 分页查询消息通知
 */
export async function notificationPageAsync(params: PagingNotificationInput,
) {
  const notificationServiceProxy = new NotificationServiceProxy();
  return notificationServiceProxy.notificationPage(params);
}

/**
 * 分页查询消息通知
 */
export async function notificationSubscriptionPageAsync(params: PagingNotificationSubscriptionInput,
) {
  const notificationServiceProxy = new NotificationServiceProxy();
  return notificationServiceProxy.notificationSubscriptionPage(params);
}

export async function setReadAsync(request) {
  const _notificationServiceProxy = new NotificationServiceProxy();
  await _notificationServiceProxy.read(request);
}


/**
 * 创建消息通知
 */
export async function sendNotificationAsync({ params }) {
  const notificationServiceProxy = new NotificationServiceProxy();
  if (params.messageLevel == 10) {
    await notificationServiceProxy.sendCommonWarningMessage(params);
  }
  if (params.messageLevel == 20) {
    await notificationServiceProxy.sendCommonInformationMessage(params);
  }
  if (params.messageLevel == 30) {
    await notificationServiceProxy.sendCommonErrorMessage(params);
  }
}


/**
 * 创建消息通知
 */
export async function sendNotificationSubscriptionAsync({ params }) {
  const notificationServiceProxy = new NotificationServiceProxy();
  if (params.messageLevel == 10) {
    await notificationServiceProxy.sendBroadCastWarningMessage(params);
  }
  if (params.messageLevel == 20) {
    await notificationServiceProxy.sendBroadCastInformationMessage(params);
  }
  if (params.messageLevel == 30) {
    await notificationServiceProxy.sendBroadCastErrorMessage(params);
  }
}

/**
 * 获取用户列表
 * @param params
 * @returns
 */
export async function getUserListAsync(
  params: PagingUserListInput,
) {
  const _userServiceProxy = new UsersServiceProxy();
  return _userServiceProxy.list(params);
}
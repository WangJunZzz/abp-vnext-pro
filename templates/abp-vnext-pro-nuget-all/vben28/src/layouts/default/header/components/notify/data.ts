import {
  NotificationServiceProxy,
  PagingNotificationListOutput,
  PagingNotificationListInput,
  PagingNotificationListOutputPagedResultDto,
} from '/@/services/ServiceProxies';
export interface ListItem {
  id: string;
  avatar: string;
  // 通知的标题内容
  title: string;
  // 是否在标题上显示删除线
  titleDelete?: boolean;
  datetime: string;
  type: string;
  read?: boolean;
  description: string;
  clickClose?: boolean;
  extra?: string;
  color?: string;
}

export interface TabItem {
  key: string;
  name: string;
  list?: PagingNotificationListOutput[];
  unreadlist?: ListItem[];
}

export const tabListData: TabItem[] = [
  {
    key: '1',
    name: '消息',
    list: [],
  },
  {
    key: '2',
    name: '通知',
    list: [],
  },
];

export async function getTextAsync(): Promise<PagingNotificationListOutputPagedResultDto> {
  let request = new PagingNotificationListInput();
  request.pageSize = 5;
  const _notificationServiceProxy = new NotificationServiceProxy();
  return await _notificationServiceProxy.common(request);
}

export async function getBroadCastAsync(): Promise<PagingNotificationListOutputPagedResultDto> {
  let request = new PagingNotificationListInput();
  request.pageSize = 5;
  const _notificationServiceProxy = new NotificationServiceProxy();
  return await _notificationServiceProxy.broadCast(request);
}

export async function setReadAsync(request) {
  const _notificationServiceProxy = new NotificationServiceProxy();
  await _notificationServiceProxy.read(request);
}

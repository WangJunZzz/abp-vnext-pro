import {
  NotificationServiceProxy,
  PagingNotificationOutput,
  PagingNotificationInput,
  PagingNotificationSubscriptionInput,
  PagingNotificationOutputPagedResultDto,
} from '/@/services/ServiceProxies';
import { useUserStoreWithOut } from '/@/store/modules/user';
const userStore = useUserStoreWithOut();
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
  list?: PagingNotificationOutput[];
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

export async function getTextAsync(): Promise<PagingNotificationOutputPagedResultDto> {
  let request = new PagingNotificationInput();
  request.pageSize = 5;
  request.pageIndex = 1;
  request.receiverUserId  = userStore.getUserInfo.userId as string;
  request.messageType = 20;
  const _notificationServiceProxy = new NotificationServiceProxy();
  return await _notificationServiceProxy.notificationPage(request);
}

export async function getBroadCastAsync(): Promise<PagingNotificationOutputPagedResultDto> {
  let request = new PagingNotificationInput();
  request.pageSize = 5;
  request.pageIndex = 1;
  request.messageType = 10;
  const _notificationServiceProxy = new NotificationServiceProxy();
  return await _notificationServiceProxy.notificationPage(request);
}

export async function setReadAsync(request) {
  const _notificationServiceProxy = new NotificationServiceProxy();
  await _notificationServiceProxy.read(request);
}


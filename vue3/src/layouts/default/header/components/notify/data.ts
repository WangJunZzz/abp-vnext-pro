// import { NotificationServiceProxy, QueryTextNotificationInput, QueryTextNotificationOutputBeePagedResultDto, QueryTextNotificationOutput } from "/@/services/ServiceProxies";

// export interface ListItem {
//   id: string;
//   title: string;
//   datetime: string;
//   type: string;
//   read?: boolean;
//   description: string;
//   clickClose?: boolean;
//   extra?: string;
//   color?: string;
// }

// export interface TabItem {
//   key: string;
//   name: string;
//   list?: QueryTextNotificationOutput[];
//   unreadlist?: ListItem[];
// }

// export const tabListData: TabItem[] = [
//   {
//     key: '1',
//     name: '消息',
//     list: [],
//   },
//   {
//     key: '2',
//     name: '通知',
//     list: [],
//   },

// ];

// /**
//  * 分页查询普通消息
//  * @param params
//  * @returns
//  */
// export async function getTextAsync(): Promise<QueryTextNotificationOutputBeePagedResultDto> {

//   let request = new QueryTextNotificationInput();
//   request.pageSize = 5;
//   const _notificationServiceProxy = new NotificationServiceProxy();
//   return await _notificationServiceProxy.text(request);
// }

// export async function getBroadCastAsync(): Promise<QueryTextNotificationOutputBeePagedResultDto> {
//   let request = new QueryTextNotificationInput();
//   request.pageSize = 5;
//   const _notificationServiceProxy = new NotificationServiceProxy();
//   return await _notificationServiceProxy.broadCast(request);
// }

// export async function setReadAsync(request) {
//   const _notificationServiceProxy = new NotificationServiceProxy();
//   await _notificationServiceProxy.read(request);
// }

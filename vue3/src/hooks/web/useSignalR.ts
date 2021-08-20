import * as signalR from "@microsoft/signalr";
import { useMessage } from '/@/hooks/web/useMessage';
import { useUserStoreWithOut } from '/@/store/modules/user';
export function useSignalR() {

  /**
   * 开始连接SignalR
   */
  function startConnect(): void {
    let connection = connectionsignalR()
    //接收普通文本消息
    connection.on("ReceiveTextMessageAsync", ReceiveTextMessageHandlerAsync);
    //接收广播消息
    connection.on("ReceiveBroadCastMessageAsync", ReceiveBroadCastMessageHandlerAsync);
    //开始连接
    connection.start().catch((err) => { console.error('SignalR连接失败:' + err) });
    // 当连接关闭时,尝试重新连接
    connection.onclose(() => {
      try {
        connection.start();
        console.info('尝试重新连接成功');
      } catch (err) {
        setTimeout(() => {
          connection = connectionsignalR();
        }, 5000);
      }
    })
  }

  /**
   * 连接signalr
   */
  function connectionsignalR(): signalR.HubConnection {
    const userStore = useUserStoreWithOut();
    const token = userStore.getToken;
    const url = import.meta.env.VITE_API_URL as string + '/signalr/notification';
    const connection = new signalR.HubConnectionBuilder().withUrl(url, { accessTokenFactory: () => token }).withAutomaticReconnect([1000, 3000, 5000, 8000, 10000, 15000]).build();
    return connection;
  }

  /**
   * 接收文本消息
   * @param message 消息体
   */
  function ReceiveTextMessageHandlerAsync(message: any) {
    console.log(message);

    const { notification } = useMessage();

    notification.open({
      message: message.title,
      description: message.content,
    });
  }


  /**
   * 接收广播消息
   * @param message 消息体
   */
  function ReceiveBroadCastMessageHandlerAsync(message: any) {
    const { notification } = useMessage();

    notification.open({
      message: message.title,
      description: message.content,
    });

  }

  return { startConnect }
}

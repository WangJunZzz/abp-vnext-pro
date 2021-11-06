import * as signalR from '@microsoft/signalr';
import { useMessage } from '/@/hooks/web/useMessage';
import { useUserStoreWithOut } from '/@/store/modules/user';
export function useSignalR() {
  /**
   * 开始连接SignalR
   */
  function startConnect(): void {
    let connection = connectionsignalR();
    //接收普通文本消息
    connection.on('ReceiveTextMessageAsync', ReceiveTextMessageHandlerAsync);
    //接收广播消息
    connection.on('ReceiveBroadCastMessageAsync', ReceiveBroadCastMessageHandlerAsync);
    //开始连接
    connection.start();
  }

  /**
   * 连接signalr
   */
  function connectionsignalR(): signalR.HubConnection {
    const userStore = useUserStoreWithOut();
    const token = userStore.getToken;

    const url = (import.meta.env.VITE_API_URL as string) + '/signalr/notification';
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(url, {
        accessTokenFactory: () => token,
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .withAutomaticReconnect({
        nextRetryDelayInMilliseconds: (retryContext) => {
          //重连规则：重连次数<300：间隔1s;重试次数<3000:间隔3s;重试次数>3000:间隔30s
          let count = retryContext.previousRetryCount / 300;
          if (count < 1) {
            //重试次数<300,间隔1s
            return 1000;
          } else if (count < 10) {
            //重试次数>300:间隔5s
            return 1000 * 5;
          } //重试次数>3000:间隔30s
          else {
            return 1000 * 30;
          }
        },
      })
      .configureLogging(signalR.LogLevel.Debug)
      .build();
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

  return { startConnect };
}

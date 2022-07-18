import * as signalR from '@microsoft/signalr';
import { useMessage } from '/@/hooks/web/useMessage';
import { useUserStoreWithOut } from '/@/store/modules/user';
let connection:signalR.HubConnection;
export function useSignalR() {

  /**
   * 开始连接SignalR
   */
  async function startConnect() {
     try {
      connectionsignalR();
      await connection.start();
      } catch (err) {
      console.log(err);
      setTimeout(() => startConnect(), 5000);
    }
  }

  /**
   * 关闭SignalR连接
   */
  function closeConnect(): void {8
    if (connection) {
      connection.stop();
    }
  }

  async function connectionsignalR(){
    const userStore = useUserStoreWithOut();
    const token = userStore.getToken;
    const url = (import.meta.env.VITE_WEBSOCKE_URL as string) + '/signalr/notification';
    connection = new signalR.HubConnectionBuilder()
    .withUrl(url, {
      accessTokenFactory: () => token,
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets,
    })  
    .withAutomaticReconnect({
      nextRetryDelayInMilliseconds: retryContext => {

        //重连规则：重连次数<300：间隔1s;重试次数<3000:间隔3s;重试次数>3000:间隔30s
        let count = retryContext.previousRetryCount / 300;
        if (count < 1)//重试次数<300,间隔1s
        {
          return 1000;
        }
        else if (count < 10) //重试次数>300:间隔5s
        {
          return 1000 * 5;

        }
        else //重试次数>3000:间隔30s
        {
          return 1000 * 30;
        }
      }
    })
    .configureLogging(signalR.LogLevel.Debug)
    .build();   

      //接收普通文本消息
      connection.on('ReceiveTextMessageAsync', ReceiveTextMessageHandlerAsync);
      //接收广播消息
      connection.on('ReceiveBroadCastMessageAsync', ReceiveBroadCastMessageHandlerAsync);
  }

  /**
   * 接收文本消息
   * @param message 消息体
   */
  function ReceiveTextMessageHandlerAsync(message: any) {
    console.log(message);
    const { notification } = useMessage();
    if (message.messageLevel == 10) {
      notification.error({
        message: message.title,
        description: message.content,
      });
    } else if (message.messageLevel == 20) {
      notification.warn({
        message: message.title,
        description: message.content,
      });
    } else if (message.messageLevel == 30) {
      notification.error({
        message: message.title,
        description: message.content,
      });
    } else {
      notification.info({
        message: message.title,
        description: message.content,
      });
    }
  }

  /**
   * 接收广播消息
   * @param message 消息体
   */
  function ReceiveBroadCastMessageHandlerAsync(message: any) {
  
    const { notification } = useMessage();
    if (message.messageLevel == 10) {
      notification.error({
        message: message.title,
        description: message.content,
      });
    } else if (message.messageLevel == 20) {
      notification.warn({
        message: message.title,
        description: message.content,
      });
    } else if (message.messageLevel == 30) {
      notification.error({
        message: message.title,
        description: message.content,
      });
    } else {
      notification.info({
        message: message.title,
        description: message.content,
      });
    }
  }

  return { startConnect, closeConnect };
}

using Microsoft.AspNetCore.SignalR;
using SuperTerminal.Const;
using SuperTerminal.Model.InstantMessage;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace SuperTerminal.MessageHandler.Instant
{
    public class InstantMessage : Hub<IInstantMessage>
    {
        public static ConcurrentDictionary<int, string> Mapping = new();
        public override Task OnConnectedAsync()
        {
            lock (Mapping)
            {
                if (Context.GetHttpContext().Items.ContainsKey(HttpItem.UserId))
                {
                    int userid = int.Parse(Context.GetHttpContext().Items[HttpItem.UserId].ToString());
                    if (!Mapping.TryAdd(userid, Context.ConnectionId))
                    {
                        Mapping.TryRemove(userid, out string value);
                        Mapping.TryAdd(userid, Context.ConnectionId);
                    }
                    Clients.Caller.ReceiveNotice(new NoticeMessage() { SenderName = "总控制", Content = "连接成功" });
                    return base.OnConnectedAsync();
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            lock (Mapping)
            {
                System.Collections.Generic.KeyValuePair<int, string> item = Mapping.FirstOrDefault(o => o.Value == Context.ConnectionId);
                Mapping.TryRemove(item.Key, out string v);
            }
            return base.OnDisconnectedAsync(exception);
        }
        /// <summary>
        /// 发送消息普通消息
        /// </summary>
        /// <param name="message">通知消息</param>
        /// <returns></returns>
        public async Task SendNotice(NoticeMessage message)
        {
            if (Mapping.TryGetValue(message.Receiver, out string connectionId))
            {
                await Clients.Client(connectionId).ReceiveNotice(message);
            }
        }
        /// <summary>
        /// 打开终端
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendOpenTerminal(OpenTerminalMessage message)
        {
            if (Context.GetHttpContext().Items[HttpItem.UserType].Equals("999"))
            {
                if (Mapping.TryGetValue(message.Receiver, out string connectionId))
                {
                    await Clients.Client(connectionId).ReceiveOpenTerminal(message);
                }
            }
        }
        /// <summary>
        /// 打开终端的情况下执行终端命令
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendExecTerminalCmd(ExecuteTerminalCommandMessage message)
        {
            if (Context.GetHttpContext().Items[HttpItem.UserType].Equals("999"))
            {
                if (Mapping.TryGetValue(message.Receiver, out string connectionId))
                {
                    await Clients.Client(connectionId).ReceiveExecTerminalCmd(message);
                }
            }

        }
        /// <summary>
        /// 发送关闭终端命令
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendCloseTerminal(CloseTerminalMessage message)
        {
            if (Context.GetHttpContext().Items[HttpItem.UserType].Equals("999"))
            {
                if (Mapping.TryGetValue(message.Receiver, out string connectionId))
                {
                    await Clients.Client(connectionId).ReceiveCloseTerminal(message);
                }
            }
        }
    }
}

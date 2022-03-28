using Microsoft.AspNetCore.SignalR.Client;
using SuperTerminal.Model.InstantMessage;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SuperTerminal.Utity
{
    /// <summary>
    /// 必须单例
    /// </summary>
    public class SignalRClient : IDisposable
    {
        private class ReciveHandler
        {
            public string methodName { get; set; }

            public Type[] parameterTypes { get; set; }

            public Func<object[], Task> handler { get; set; }
        }

        private List<ReciveHandler> reciveHandlers = new List<ReciveHandler>();
        private static readonly object ObjConnectionLock = new object();
        private readonly IApiHelper _apiHelper;
        /// <summary>
        /// 初始化传入IApiHelper对象
        /// </summary>
        /// <param name="apiHelper"></param>
        public SignalRClient(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
        private event Func<Exception, Task> ClosedEvent = null;
        private HubConnection connection;
        /// <summary>
        /// 同步连接
        /// </summary>
        /// <returns></returns>
        public void StartConnection()
        {
            if (connection != null && connection.State == HubConnectionState.Disconnected)
            {
                connection.DisposeAsync().GetAwaiter().GetResult();
                connection = null;
            }
            if (connection == null)
            {
                try
                {
                    var token = _apiHelper.GetToken();
                    if (string.IsNullOrEmpty(token))
                    {
                        throw new Exception("Token获取失败");
                    }
                    connection = new HubConnectionBuilder()
                    .WithUrl(string.Concat(_apiHelper.Domain, "/instantMessage"),
                    (o) =>
                    {
                        o.Headers.Add("Token", string.Concat("SuperTerminal ", token));
                    }
                    )
                    .Build();
                    connection.Closed += Connection_ClosedAsync;
                    if (reciveHandlers.Count > 0)
                    {
                        foreach (var item in reciveHandlers)
                        {
                            connection.On(item.methodName, item.parameterTypes, item.handler);
                        }
                    }
                    connection.StartAsync(CancellationToken.None).Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("获取连接状态失败,程序将在5秒后重新连接,重新连接倒计时");
                    for (int i = 0; i < 5; i++)
                    {
                        Thread.Sleep(1000);
                        Console.WriteLine(i + 1);
                    }
                    StartConnection();
                }
            }
        }
        /// <summary>
        /// 注册接收消息事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reciveCommandhandler"></param>
        public void AddReceiveHandler<T>(string methedName, Action<T> reciveCommandhandler) where T : IMessage
        {
            reciveHandlers.Add(new ReciveHandler()
            {
                methodName = methedName,
                parameterTypes = new Type[] { typeof(T) },
                handler = (obj) =>
                {
                    return Task.Factory.StartNew(() => {
                        reciveCommandhandler.DynamicInvoke(obj[0]);
                    });
                }
            });
            connection.On(methedName, reciveCommandhandler);
        }
        public void SendMsg<T>(string methedName, T msg) where T : IMessage
        {
            connection.SendAsync(methedName, msg).Wait();
        }
        public void RemoveReceiveHandler(string methedName)
        {
            reciveHandlers.RemoveAll(o => o.methodName == methedName);
            connection.Remove(methedName);
        }
        /// <summary>
        /// 连接断开后一直尝试重新连接
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task Connection_ClosedAsync(Exception arg)
        {
            await Task.Factory.StartNew(() =>
            {
                lock (ObjConnectionLock)
                {
                    StartConnection();
                }
            });
        }
        public void Dispose()
        {
            if (connection != null)
            {
                if (ClosedEvent != null)
                {
                    connection.Closed -= ClosedEvent;
                }
                connection.Closed -= Connection_ClosedAsync;
                connection.DisposeAsync().GetAwaiter();
            }
            GC.SuppressFinalize(this);
        }
    }
}

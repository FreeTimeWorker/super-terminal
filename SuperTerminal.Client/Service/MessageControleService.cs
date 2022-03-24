using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SuperTerminal.Model.InstantMessage;
using SuperTerminal.Utity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperTerminal.Client
{
    public class MessageControleService: BackgroundService
    {
        private static object teminalLock = new object();
        ManualResetEvent resetEvent = new ManualResetEvent(false);//阻止服务自动退出
        private CancellationTokenSource cancelSource;//关闭服务后退出服务
        private ConcurrentDictionary<int, InstantCmdService> _terminalMap = new ConcurrentDictionary<int, InstantCmdService>();
        private readonly Config _config;
        private readonly SignalRClient _signalRClient;
        private readonly LogServer _logServer;
        private readonly IConfiguration _configuration;
        private readonly OsHelper _osHelper;
        public MessageControleService(IConfiguration configuration, SignalRClient signalRClient,LogServer logServer,OsHelper osHelper)
        {
            configuration.Bind(_config);
            _signalRClient = signalRClient;
            _logServer = logServer;
            _configuration = configuration;
            _osHelper = osHelper;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(() =>
            {
                if (!cancelSource.Token.IsCancellationRequested)
                {
                    //打开连接,接收消息
                    _signalRClient.AddReceiveHandler<NoticeMessage>("ReceiveNotice", o => {
                        Task.Factory.StartNew(() =>
                        {
                            _logServer.Write($"{o.SenderName}:{o.Content}");
                        });
                    });
                    //收到打开终端的消息
                    _signalRClient.AddReceiveHandler<OpenTerminalMessage>("ReceiveOpenTerminal", msg => {
                        lock (teminalLock)
                        {
                            //打开终端
                            if (!_terminalMap.ContainsKey(msg.Sender))
                            {
                                InstantCmdService InstantCmdService = new InstantCmdService(_configuration, _logServer, _signalRClient, _osHelper);
                                InstantCmdService.StartControle(msg);
                                _terminalMap.TryAdd(msg.Sender, InstantCmdService);
                            }
                            else if (_terminalMap.TryGetValue(msg.Sender, out InstantCmdService _terminal))
                            {
                                if (_terminal == null || _terminal.Process == null)
                                {
                                    InstantCmdService InstantCmdService = new InstantCmdService(_configuration, _logServer, _signalRClient, _osHelper);
                                    InstantCmdService.StartControle(msg);
                                    _terminalMap.TryAdd(msg.Sender, InstantCmdService);
                                }
                            }
                            _signalRClient.SendMsg<NoticeMessage>("SendNotice", new NoticeMessage()
                            {
                                Mark = NoticeMessageMark.SuperTerminal,
                                SenderName = _config.NickName,
                                Content = "终端打开成功",
                                NeedReply = false,
                                Receiver = msg.Sender,
                                Sender = msg.Receiver
                            });
                        }
                    });
                    //打开终端的情况下输入命令
                    _signalRClient.AddReceiveHandler<ExecuteTerminalCommandMessage>("ReceiveExecTerminalCmd", msg => {
                        if (_terminalMap.TryGetValue(msg.Sender, out InstantCmdService _terminal))
                        {
                            if (_terminal != null && _terminal.Process != null)
                            {
                                _terminal.ReceiveExecTerminalCmd(msg);
                            }
                        }
                    });
                    //关闭终端
                    _signalRClient.AddReceiveHandler<CloseTerminalMessage>("ReceiveCloseTerminal", msg => {
                        //打开终端
                        lock (teminalLock)
                        {
                            if (_terminalMap.TryGetValue(msg.Sender, out InstantCmdService _terminal))
                            {
                                if (_terminal != null || _terminal.Process != null)
                                {
                                    ExecuteTerminalCommandMessage exitcmd = new ExecuteTerminalCommandMessage()
                                    {
                                        Content = "exit;",
                                        NeedReply = false,
                                        Receiver = msg.Receiver,
                                        Sender = msg.Sender,
                                        SenderName = msg.SenderName
                                    };
                                    //尝试通过正常流程退出
                                    _terminal.ReceiveExecTerminalCmd(exitcmd);
                                }
                                //清理终端
                                _terminalMap.TryRemove(msg.Sender, out var removedTerminal);
                                _signalRClient.SendMsg<NoticeMessage>("SendNotice", new NoticeMessage()
                                {
                                    Mark = NoticeMessageMark.SuperTerminal,
                                    SenderName = _config.NickName,
                                    Content = "终端结束",
                                    NeedReply = false,
                                    Receiver = msg.Sender,
                                    Sender = msg.Receiver
                                });
                            }
                        }
                    });
                    resetEvent.WaitOne();
                }
            }, cancelSource.Token);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            cancelSource = new CancellationTokenSource();
            cancelSource.Token.Register(() =>
            {
                resetEvent.Set();
                _logServer.Write($"--服务停止后执行--");
                _logServer.Write($"服务停止");
                _signalRClient.Dispose();
                _logServer.Write($"连接断开");
                _logServer.Write($"日志服务停止");
                _logServer.Dispose();
            });
            _logServer.Write($"服务启动");
            return base.StartAsync(cancelSource.Token);
        }
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            cancelSource.Cancel();
            await base.StopAsync(cancelSource.Token);
        }
    }
}

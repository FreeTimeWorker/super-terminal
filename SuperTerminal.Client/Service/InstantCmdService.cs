using Microsoft.Extensions.Configuration;
using SuperTerminal.Model.InstantMessage;
using SuperTerminal.Utity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperTerminal.Client
{
    /// <summary>
    /// 即时命令服务
    /// </summary>
    public class InstantCmdService : IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly LogServer _logServer;
        private readonly SignalRClient _signalRClient;
        private readonly OsHelper _osHelper;
        /// <summary>
        /// 是否正在运行
        /// </summary>
        private bool _run = true;
        /// <summary>
        /// 是否已经完全释放
        /// </summary>
        private bool _disposed = false;
        /// <summary>
        /// 接收到的命令
        /// </summary>
        private string _command { get; set; }
        private Task _mainTask { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Process Process { get; set; }
        /// <summary>
        /// 终端关联信息
        /// </summary>
        private OpenTerminalMessage _terminalInfo { get; set; }
        /// <summary>
        /// 命令控制
        /// </summary>
        private EventWaitHandle _commandExecWait = new EventWaitHandle(false, EventResetMode.AutoReset);
        /// <summary>
        /// 主线程控制
        /// </summary>
        private EventWaitHandle _mainProcessEventWait = new EventWaitHandle(false, EventResetMode.ManualReset);
        public InstantCmdService(IConfiguration configuration, LogServer logServer, SignalRClient signalRClient, OsHelper osHelper)
        {
            _logServer = logServer;
            _signalRClient = signalRClient;
            _osHelper = osHelper;
            _configuration = configuration;
        }

        public void Dispose()
        {
            try
            {
                _logServer.Write($"超级终端关闭开始执行");
                Process.Close();
                Process.Dispose();
                _mainTask.Dispose();
            }
            catch (Exception ex)
            {
                _logServer.Write($"超级终端关闭:{ex.Message}");
            }
        }
        /// <summary>
        /// 开启控制
        /// </summary>
        /// <param name="terminalInfo"></param>
        public void StartControle(OpenTerminalMessage terminalInfo)
        {
            _terminalInfo = terminalInfo;
            _mainTask = Task.Factory.StartNew(() =>
            {
                StartTerminal();
            });
        }
        //开启终端
        private void StartTerminal()
        {
            string filename = "cmd.exe";
            if (_osHelper.OSPlatformEnum == Enum.OSPlatformEnum.Linux)
            {
                filename = "/bin/bash";
            }
            var psi = new ProcessStartInfo()
            {
                FileName = filename,
                CreateNoWindow = true,
                WorkingDirectory = AppContext.BaseDirectory,
                UseShellExecute = false,
                RedirectStandardInput = true,//重定向输入流
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
            Process = Process.Start(psi);
            var sinput = Task.Factory.StartNew(() =>
            {
                using (StreamWriter sw = Process.StandardInput)
                {
                    while (_run)
                    {
                        _commandExecWait.WaitOne();
                        _logServer.Write($"超级终端执行命令:{_command}");
                        sw.WriteLine(_command);
                        if (_command == "exit" || _command == "exit;")
                        {
                            _run = false;
                            _mainProcessEventWait.Set();
                        }
                        while (!_run && !_disposed)
                        {
                            sw.WriteLine("exit");
                            Thread.Sleep(1000);
                        }
                    }
                }

            });
            var serror = Task.Factory.StartNew(() =>
            {
                using (StreamReader sr = Process.StandardError)
                {
                    while (sr.BaseStream.CanRead && !sr.EndOfStream)
                    {
                        string s = sr.ReadLine();
                        if (!_run)
                        {
                            //运行停止信号
                            Process.StandardError.Close();
                        }
                        _logServer.Write($"超级终端执行错误输出:{s}");
                        _signalRClient.SendMsg<NoticeMessage>("SendNotice", new NoticeMessage()
                        {
                            SenderName = _configuration["NickName"],
                            Content = s,
                            NeedReply = false,
                            Receiver = _terminalInfo.Sender,
                            Sender = _terminalInfo.Receiver
                        });
                    }
                }
            });
            var sout = Task.Factory.StartNew(() =>
            {
                using (StreamReader sr = Process.StandardOutput)
                {
                    while (sr.BaseStream.CanRead && !sr.EndOfStream)
                    {
                        string s = sr.ReadLine();
                        if (!_run)
                        {
                            Process.StandardOutput.Close();
                        }
                        else
                        {
                            _logServer.Write($"超级终端执行标准输出:{s}");
                            //通知发送者执行结果
                            _signalRClient.SendMsg<NoticeMessage>("SendNotice", new NoticeMessage()
                            {
                                SenderName = _configuration["NickName"],
                                Content = s,
                                NeedReply = false,
                                Receiver = _terminalInfo.Sender,
                                Sender = _terminalInfo.Receiver
                            });

                        }
                    }
                }
            });
            _mainProcessEventWait.WaitOne();
            Task.WaitAll(serror, sout);
            _disposed = true;
            this.Dispose();
        }
        /// <summary>
        /// 收到命令后把命令放入当前终端的输入流
        /// </summary>
        /// <param name="message"></param>
        public void ReceiveExecTerminalCmd(ExecuteTerminalCommandMessage message)
        {
            this._command = message.Content;
            _commandExecWait.Set();
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperTerminal.GlobalService;
using SuperTerminal.Utity;
using System;
using System.IO;
using System.Security.Cryptography;

namespace SuperTerminal.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            args = new string[] { "purpose:regist", "Address=http://localhost:5000", "NickName=测试1" };
            args = new string[] { "purpose:WindowsServer" };
            if (args.Length == 0)
            {
                Console.WriteLine("请勿直接执行");
                return;
            }
            else
            {
                switch (args[0])
                {
                    case "purpose:regist":
                        Init(args);
                        break;
                    case "purpose:WindowsServer":
                        RunAsWindowsServer(args);
                        break;
                    default:
                        Console.WriteLine("请勿直接执行");
                        break;
                }
            }
        }
        private async static void Init(string[] args)
        {
            await Host.CreateDefaultBuilder()
                .ConfigureLogging((hostingContext, logging) => {
                    logging.AddFilter(log => log == LogLevel.Error);
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", true, reloadOnChange: true);
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddHttpClient();
                    services.AddTransient<IApiHelper, ApiHelper>();
                    services.AddSingleton<OsHelper>();
                    services.AddSingleton<Codebook>();
                    services.AddSingleton<LogServer>();
                    services.AddHostedService<Init>();
                })
                .UseConsoleLifetime().RunConsoleAsync(op => op.SuppressStatusMessages = true);
        }
        private async static void RunAsWindowsServer(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName = "SuperTerminal_Client";
                })
                .ConfigureServices(services =>
                {
                    services.AddHttpClient();
                    services.AddTransient<IApiHelper, ApiHelper>();
                    services.AddSingleton<OsHelper>();
                    services.AddSingleton<Codebook>();
                    services.AddSingleton<LogServer>();
                    services.AddSingleton<SignalRClient>();//signalr
                    services.AddTransient<InstantCmdService>();//每次获取都必须不同
                    services.AddHostedService<MessageControleService>();
                    ServiceAgent.Provider = services.BuildServiceProvider();
                })
                .Build().RunAsync();
        }
    }
}

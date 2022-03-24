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
            //args = new string[] { "purpose:regist", "Address=http://localhost:5000", "NickName=测试1" };
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
                    services.AddHostedService<Init>();
                    services.AddTransient<OsHelper>();
                    services.AddSingleton<Codebook>();
                    services.AddSingleton<LogServer>();
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
                    services.AddTransient<OsHelper>();
                    services.AddSingleton<Codebook>();
                    services.AddSingleton<LogServer>();
                    services.AddSingleton<SignalRClient>();//signalr
                    services.AddTransient<InstantCmdService>();
                    services.AddHostedService<MessageControleService>();
                    ServiceAgent.Provider = services.BuildServiceProvider();
                })
                .Build().RunAsync();
        }
    }
}

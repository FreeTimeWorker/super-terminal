using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperTerminal.GlobalService;
using SuperTerminal.Utity;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SuperTerminal.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //args = new string[] { "purpose:systemd" };
            //args = new string[] { "purpose:regist", "Address=http://192.168.3.154:8086", "NickName=测试1" };
            //args = new string[] { "purpose:WindowsServer" };
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
                        Init(args).Wait();
                        break;
                    case "purpose:WindowsServer":
                        RunAsWindowsServer(args).Wait();
                        break;
                    case "purpose:systemd":
                        RunAsSystemd(args).Wait();
                        break;
                    default:
                        Console.WriteLine("请勿直接执行");
                        break;
                }
            }
        }
        private static Task Init(string[] args)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureLogging((hostingContext, logging) =>
                {
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
                .UseConsoleLifetime().RunConsoleAsync();
        }
        private static Task RunAsWindowsServer(string[] args)
        {
              return Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName = "SuperTerminal_Client";
                })
                .ConfigureHostConfiguration((config) => {
                    config.AddJsonFile("appsettings.json", true, reloadOnChange: true);
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureServices(services =>
                {
                    services.AddHttpClient();
                    services.AddTransient<IApiHelper, ApiHelper>();
                    services.AddSingleton<OsHelper>();
                    services.AddSingleton<Codebook>();
                    services.AddSingleton<LogServer>();
                    services.AddSingleton<SignalRClient>();//signalr
                    services.AddHostedService<MessageControleService>();
                })
                .Build().RunAsync();
        }
        private static Task RunAsSystemd(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSystemd()
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", true, reloadOnChange: true);
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureServices(services =>
                {
                    services.AddHttpClient();
                    services.AddTransient<IApiHelper, ApiHelper>();
                    services.AddSingleton<OsHelper>();
                    services.AddSingleton<Codebook>();
                    services.AddSingleton<LogServer>();
                    services.AddSingleton<SignalRClient>();//signalr
                    services.AddHostedService<MessageControleService>();
                })
                .Build().RunAsync();
        }
    }
}

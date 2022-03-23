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
            if (!File.Exists(Path.Combine(AppContext.BaseDirectory, "appsettings.json")))
            {
                args = new string[] { "Address=http://localhost:5000", "NickName=测试1" };
                Host.CreateDefaultBuilder()
                    .ConfigureLogging((hostingContext, logging) => {
                        logging.AddFilter(log => log == LogLevel.Error);
                    })
                    .ConfigureAppConfiguration((hostContext, config) =>
                    {
                        var env = hostContext.HostingEnvironment;
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
                    })
                    .UseConsoleLifetime().RunConsoleAsync(op => op.SuppressStatusMessages = true);
            }
        }


    }
}

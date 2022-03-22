using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperTerminal.Utity;
using System;
using System.IO;
using System.Windows.Forms;

namespace SuperTerminal.Manager
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;
            Application.Run(ServiceProvider.GetRequiredService<Main>());
        }
        public static IServiceProvider ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, reloadOnChange: true)
                .Build();
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddHttpClient();//IHttpClientFactory
                    services.AddSingleton<IConfiguration>(config);
                    services.AddTransient<Login>();
                    services.AddTransient<Regist>();
                    services.AddTransient<Setting>(); 
                    services.AddTransient<Main>();
                    services.AddSingleton<IApiHelper, ApiHelper>();
                    services.AddSingleton<SignalRClient>();
                });
        }
    }
}

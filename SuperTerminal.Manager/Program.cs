using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sunny.UI;
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
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) =>
            {
                if (e.Exception.GetType().Equals(typeof(UnauthorizedException)))
                {
                    UIForm uI = new UIForm();
                    uI.ShowErrorTip(e.Exception.Message);
                    return;
                }
                string str = "";
                string strDateInfo = "\r\n����Ӧ�ó���δ������쳣��" + DateTime.Now.ToString() + "\r\n";
                Exception error = e.Exception;
                if (error != null)
                {
                    string logInfo = string.Format(strDateInfo + "�쳣���ͣ�{0}\r\n�쳣��Ϣ��{1}\r\n�쳣��Ϣ��{2}\r\n", error.GetType().Name, error.Message, error.StackTrace);
                    str = string.Format(strDateInfo + "�쳣���ͣ�{0}\r\n�쳣��Ϣ��{1}\r\n",
                    error.GetType().Name, error.Message);
                }
                else
                {
                    str = string.Format("Ӧ�ó����̴߳���:{0}", e);
                }

                MessageBox.Show(str, "ϵͳ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                
                string str = "";
                Exception error = e.ExceptionObject as Exception;
                string strDateInfo = "����Ӧ�ó���δ������쳣��" + DateTime.Now.ToString() + "\r\n";
                if (error != null)
                {
                    string logInfo = string.Format(strDateInfo + "Application UnhandledException:{0};\n\r��ջ��Ϣ:{1}", error.Message, error.StackTrace);
                    str = string.Format(strDateInfo + "Application UnhandledException:{0};\n\r", error.Message);
                }
                else
                {
                    str = string.Format("Application UnhandledError:{0}", e);
                }

                MessageBox.Show(str, "ϵͳ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;
            Application.Run(ServiceProvider.GetRequiredService<Main>());
        }
        public static IServiceProvider ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((host,config) => {
                    config.AddJsonFile("appsettings.json", true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) => {
                    services.AddHttpClient();
                    services.AddTransient<Login>();
                    services.AddTransient<Regist>();
                    services.AddTransient<Setting>(); 
                    services.AddTransient<Main>();
                    services.AddTransient<Common>();
                    services.AddSingleton<IApiHelper, ApiHelper>();
                    services.AddSingleton<SignalRClient>();
                });
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SuperTerminal.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                RunAsWebServer(args);
            }
            else
            {
                RunAsWindowServer(args);
            }
        }
        /// <summary>
        /// �� web��ʽ����
        /// </summary>
        /// <param name="args"></param>
        public static void RunAsWebServer(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).Build().Run();
        }
        /// <summary>
        /// ��windows��ʽ����
        /// </summary>
        /// <param name="args"></param>
        public static void RunAsWindowServer(string[] args)
        {
             Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel()
                    .UseStartup<Startup>();
                }).Build().Run();
        }
    }
}

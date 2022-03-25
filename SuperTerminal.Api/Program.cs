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
        /// 以 web形式运行
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
        /// 以windows形式运行
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

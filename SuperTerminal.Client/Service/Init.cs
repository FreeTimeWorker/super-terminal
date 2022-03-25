using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SuperTerminal.Model.User;
using SuperTerminal.Utity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperTerminal.Client
{
    internal class Init: IHostedService
    {
        private readonly  IConfiguration _configuration;
        private readonly IApiHelper _apiHelper;
        private readonly OsHelper _osHelper;
        private readonly Codebook _codebook;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        public Init(IConfiguration configuration, IApiHelper apiHelper, OsHelper osHelper, Codebook codebook, IHostApplicationLifetime hostApplicationLifetime)
        {
            _configuration = configuration;
            _apiHelper = apiHelper;
            _osHelper = osHelper;
            _codebook = codebook;
            _hostApplicationLifetime = hostApplicationLifetime;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var address = _configuration["Address"];
                _codebook.GenratePassFile();//生成密码本
                var ivKey = _codebook.GetIVandKey();//获取密钥              
                var model = GetComputerInfo();//提交的model
                var result = _apiHelper.Post<BoolModel<int>>("/Auth/RegistEquipment", model);
                if (result == null)
                {
                    Console.WriteLine("服务器连接失败");
                }
                else
                {
                    if (result.Successed)
                    {
                        model.Id = result.Data;
                        model.PassWord = model.PassWord.MD5().AesEncrypt(ivKey.Item1, ivKey.Item2);
                        WriteConfig(model, address);
                    }
                    else
                    {
                        Console.WriteLine(result.Message);
                    }
                }
                _hostApplicationLifetime.StopApplication();
            });
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        /// <summary>
        /// 写配置
        /// </summary>
        private void WriteConfig(ViewEquipmentModel model,string Address)
        {
            var config = new Config()
            {
                Id=model.Id,
                UserName= model.UserName,
                PassWord= model.PassWord,
                NickName= model.NickName,
                Address=Address
            };
            string json = config.ToJson();
            using (FileStream fs = new FileStream("appsettings.json", FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(json);
                }
            }
            (_configuration as IConfigurationRoot).Reload();
        }

        private ViewEquipmentModel GetComputerInfo()
        {
            Console.WriteLine("正在获取计算机基本信息");
            ViewEquipmentModel model = new()
            {
                NickName = _configuration["NickName"],
                OSArchitecture = _osHelper.OSArchitecture,
                OSDescription = _osHelper.OSDescription,
                OSPlatform = _osHelper.OSPlatformEnum,
                PassWord = Guid.NewGuid().ToString().Replace("-", "").ToUpper(),
                UserName = Guid.NewGuid().ToString().Replace("-", "").ToUpper(),
                PrivIp = _osHelper.LocalIp,
                PubIp = _osHelper.PubIP
            };
            Console.WriteLine($"内网IP:{model.PrivIp}");
            Console.WriteLine($"公网IP:{model.PubIp}");
            Console.WriteLine($"系统架构:{model.OSArchitecture.ToString()}");
            Console.WriteLine($"系统类型:{model.OSPlatform.ToString()}");
            Console.WriteLine($"系统名称:{model.OSDescription}");
            return model;
        }
    }
}

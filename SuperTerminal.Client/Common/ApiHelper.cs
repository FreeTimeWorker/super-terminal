using Microsoft.Extensions.Configuration;
using SuperTerminal.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Client
{
    public class ApiHelper : IApiHelper
    {
        private static string Token = "";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly Codebook _codebook;
        private readonly LogServer _logServer;
        public ApiHelper(IConfiguration configuration, IHttpClientFactory httpClientFactory,Codebook codebook,LogServer logServer)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _codebook = codebook;
            _logServer = logServer;
        }
        public string Domain 
        { 
            get
            {
                return _configuration["Address"];
            }
        }
        public T Get<T>(string url)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                client.BaseAddress = new Uri(Domain);
                if (!string.IsNullOrEmpty(Token))
                {
                    client.DefaultRequestHeaders.Remove("Token");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Token", string.Concat("SuperTerminal ", Token));
                }
                var result = client.GetAsync(url).Result;
                if (result.Headers.Contains("ReToken"))
                {
                    Token = result.Headers.GetValues("ReToken").First();
                }
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var res = result.Content.ReadAsStringAsync().Result.ToObject<ResponseModel<T>>();
                    return res.Data;
                }
                else
                {
                    return default;
                }
            }
        }
        /// <summary>
        /// token放到内存中
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            if (string.IsNullOrEmpty(_configuration["SuperTerminal_UserName"]) || string.IsNullOrEmpty(_configuration["SuperTerminal_PassWord"]))
            {
                return string.Empty;
            }
            var ivKey = _codebook.GetIVandKey();
            _logServer.Write(_configuration["SuperTerminal_UserName"]);
            _logServer.Write(_configuration["SuperTerminal_PassWord"]);
            var data = new
            {
                UserName= _configuration["SuperTerminal_UserName"],
                Password = _configuration["SuperTerminal_PassWord"].AesDecrypt(ivKey.Item1, ivKey.Item2)
            };
            var result = this.Post<BoolModel<string>>("Auth/GetToken", data);
            if (result == null)
            {
                return string.Empty;
            }
            if (result.Successed)
            {
                Token = result.Data;
                return result.Data;
            }
            else
            {
                return string.Empty;
            }
        }
        public T Post<T>(string url, object obj)
        {
            try
            {
                using (var client = _httpClientFactory.CreateClient())
                {
                    client.BaseAddress = new Uri(Domain);
                    StringContent content = new StringContent(obj.ToJson(), Encoding.UTF8, "application/json");
                    if (!string.IsNullOrEmpty(Token))
                    {
                        client.DefaultRequestHeaders.Remove("Token");
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Token", string.Concat("SuperTerminal ", Token));
                    }
                    var result = client.PostAsync(url, content).Result;
                    if (result.Headers.Contains("ReToken"))
                    {
                        Token = result.Headers.GetValues("ReToken").First();
                    }
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var res = result.Content.ReadAsStringAsync().Result.ToObject<ResponseModel<T>>();
                        return res.Data;
                    }
                    else
                    {
                        return default;
                    }
                }
            }
            catch
            {
                return default;
            }
        }
    }
}

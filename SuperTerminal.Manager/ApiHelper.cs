using Microsoft.Extensions.Configuration;
using SuperTerminal.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SuperTerminal.Manager
{
    public class ApiHelper : IApiHelper
    {
        //这里保存提交到api的用户名和密码
        public static string UserName = "";
        public static string PassWord = "";
        private static string Token = "";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public ApiHelper(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
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
                    GetToken();
                    throw new Exception("请求发生错误");
                }
            }
        }
        /// <summary>
        /// token放到内存中
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(PassWord))
            {
                return string.Empty;
            }
            var data = new
            {
                UserName,
                PassWord
            };
            var result = this.Post<BoolModel>("Auth/GetToken", data);
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
                    else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedException("Token过期,请重新登录");
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

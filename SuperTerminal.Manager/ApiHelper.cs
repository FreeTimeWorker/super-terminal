using Microsoft.Extensions.Configuration;
using SuperTerminal.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        [Retry(3)]
        public virtual ResponseModel<T> Get<T>(string url)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                client.BaseAddress = new Uri(Domain);
                if (!string.IsNullOrEmpty(Token))
                {
                    client.DefaultRequestHeaders.Remove("Token");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Token", string.Concat("naw ", Token));
                }
                var result = client.GetAsync(url).Result;
                if (result.Headers.Contains("ReToken"))
                {
                    Token = result.Headers.GetValues("ReToken").First();
                }
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return result.Content.ReadAsStringAsync().Result.ToObject<ResponseModel<T>>();
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
            if (string.IsNullOrEmpty(Token))
            {
                var data = new
                {
                    UserName = UserName,
                    PassWord = PassWord
                };
                var result = this.Post<BoolModel>("Auth/GetToken", data);
                if (result == null)
                {
                    return null;
                }
                if (result.Status == 200 && result.Data.Successed)
                {
                    Token = result.Data.Data;
                    return result.Data.Data;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return Token;
            }
        }

        [Retry(3)]
        public virtual ResponseModel<T> Post<T>(string url, object obj)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                client.BaseAddress = new Uri(Domain);
                StringContent content = new StringContent(obj.ToJson(), Encoding.UTF8, "application/json");
                if (!string.IsNullOrEmpty(Token))
                {
                    client.DefaultRequestHeaders.Remove("Token");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Token", string.Concat("naw ", Token));
                }
                var result = client.PostAsync(url, content).Result;
                if (result.Headers.Contains("ReToken"))
                {
                    Token = result.Headers.GetValues("ReToken").First();
                }
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return result.Content.ReadAsStringAsync().Result.ToObject<ResponseModel<T>>();
                }
                else
                {
                    throw new Exception("请求发生错误");
                }
            }
        }
    }
}

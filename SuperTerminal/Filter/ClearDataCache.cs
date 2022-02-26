using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using SuperTerminal.GlobalService;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
namespace SuperTerminal.Filter
{
    public class ClearDataCache: ActionFilterAttribute
    {
        private string _key { get; set; } = "";
        public ClearDataCache()
        {
            
        }
        public ClearDataCache(string Key)
        {
            this._key = Key;
        }
        /// <summary>
        /// 执行结束执行
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var config = ServiceAgent.Provider.GetService<IConfiguration>();
            //这里保存的Key
            var keys = RedisHelper.Instance.Keys($"{config["Redis:Prefix"]}DataCache_{_key}*");
            var willDelKeys = new string[keys.Length];
            for (int i = 0; i < willDelKeys.Length; i++)
            {
                willDelKeys[i] = keys[i][config["Redis:Prefix"].Length..];
            }
            RedisHelper.Instance.Del(willDelKeys);
            base.OnActionExecuted(context);
        }
    }
}

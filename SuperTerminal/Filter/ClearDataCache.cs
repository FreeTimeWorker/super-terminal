using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperTerminal.GlobalService;
namespace SuperTerminal.Filter
{
    public class ClearDataCache : ActionFilterAttribute
    {
        private string Key { get; set; } = "";
        public ClearDataCache()
        {

        }
        public ClearDataCache(string Key)
        {
            this.Key = Key;
        }
        /// <summary>
        /// 执行结束执行
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            IConfiguration config = ServiceAgent.Provider.GetService<IConfiguration>();
            //这里保存的Key
            string[] keys = RedisHelper.Instance.Keys($"{config["Redis:Prefix"]}DataCache_{Key}*");
            string[] willDelKeys = new string[keys.Length];
            for (int i = 0; i < willDelKeys.Length; i++)
            {
                willDelKeys[i] = keys[i][config["Redis:Prefix"].Length..];
            }
            RedisHelper.Instance.Del(willDelKeys);
            base.OnActionExecuted(context);
        }
    }
}

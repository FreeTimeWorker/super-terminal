using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SuperTerminal.Const;

namespace SuperTerminal.Filter
{
    public class DataCache: ActionFilterAttribute
    {
        private string _key { get; set; } = "";
        public DataCache()
        {
            
        }
        public DataCache(string Key)
        {
            this._key = Key;
        }
        /// <summary>
        /// 进入前执行
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var salt = context.HttpContext.Items[HttpItem.UserId];
            var path = context.HttpContext.Request.Path.HasValue ? context.HttpContext.Request.Path.Value : "";
            var query = context.HttpContext.Request.QueryString.HasValue ? context.HttpContext.Request.QueryString.Value : "";
            var key = $"DataCache_{_key}_{salt}_{path}{query}";
            var jsonvalue = RedisHelper.Instance.Get(key);
            if (string.IsNullOrEmpty(jsonvalue))
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new ContentResult()
                {
                    ContentType = context.HttpContext.Response.ContentType,
                    StatusCode = 200,
                    Content = jsonvalue
                };
                return;
            }
        }
        /// <summary>
        /// 执行结束执行
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var salt = context.HttpContext.Items[HttpItem.UserId];
            var path = context.HttpContext.Request.Path.HasValue ? context.HttpContext.Request.Path.Value : "";
            var query = context.HttpContext.Request.QueryString.HasValue ? context.HttpContext.Request.QueryString.Value : "";
            var key = $"DataCache_{_key}_{salt}_{path}{query}";
            if (context.Result != null)
            {
                RedisHelper.Instance.Set(key, ((Microsoft.AspNetCore.Mvc.ObjectResult)context.Result).Value, CacheSetting.DataCacheTimeOut * 60);
            }
            base.OnActionExecuted(context);
        }
    }
}

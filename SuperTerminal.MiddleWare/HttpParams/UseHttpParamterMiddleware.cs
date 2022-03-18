using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SuperTerminal.MiddleWare
{
    public class UseHttpParamterMiddleware
    {
        private readonly RequestDelegate _next;

        public UseHttpParamterMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task InvokeAsync(HttpContext context)
        {
            if (context.Request.ContentType == "application/json")
            {
                context.Request.EnableBuffering();
                using StreamReader reader = new(context.Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: 1024 * 20, leaveOpen: true);
                string body = reader.ReadToEndAsync().Result;
                Utf8JsonReader utf8JsonReader = new(Encoding.UTF8.GetBytes(body));
                try
                {
                    if (!string.IsNullOrEmpty(body) && JsonDocument.TryParseValue(ref utf8JsonReader, out JsonDocument jsonDocument))
                    {
                        if (jsonDocument.RootElement.ValueKind == JsonValueKind.Object)
                        {
                            if (jsonDocument.RootElement.TryGetProperty("current", out JsonElement pageElem) && pageElem.TryGetInt32(out int pageValue))
                            {
                                context.Items.Add("current", pageValue.ToString());
                            }
                            else
                            {
                                context.Items.Add("current", "1");
                            }

                            if (jsonDocument.RootElement.TryGetProperty("limit", out JsonElement limitElem) && limitElem.TryGetInt32(out int limitValue))
                            {
                                context.Items.Add("pageSize", limitValue.ToString());
                            }
                            else
                            {
                                context.Items.Add("pageSize", "20");
                            }
                        }
                        else
                        {
                            context.Items.Add("current", "1");
                            context.Items.Add("pageSize", "20");
                        }
                    }
                    else
                    {
                        context.Items.Add("current", "1");
                        context.Items.Add("pageSize", "20");
                    }
                }
                catch (System.Exception ex)
                {
                    context.Response.StatusCode = 402;
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync($"{{\"Status\":301,\"StatusMsg\":\"json模型错误\",\"Data\":\"{ex.Message}\"}}");
                }
                context.Request.Body.Position = 0;
            }
            return _next(context);
        }
    }
    /// <summary>
    /// 扩展方法，将中间件暴露出去
    /// </summary>
    public static class UseHttpParamterMiddlewareExtensions
    {
        /// <summary>
        /// httpparamter中间件，分页补充内容，添加对application/json提交的数据分页的支持
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHttpParamter(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UseHttpParamterMiddleware>();
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SuperTerminal.JWT
{
    public class UseJwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtConfig _jwtConfig = new();
        private readonly IJwt _jwt;
        public UseJwtMiddleware(RequestDelegate next, IConfiguration configration, IJwt jwt)
        {
            _next = next;
            _jwt = jwt;
            configration.GetSection("Jwt").Bind(_jwtConfig);
        }

        private static Regex GetIgnoreUrlsReg(List<string> urls)
        {
            List<string> regStrs = new();
            foreach (string item in urls)
            {
                regStrs.Add(string.Concat("^", item, ".*$"));
            }
            return new Regex(string.Join("|", regStrs));
        }

        public Task InvokeAsync(HttpContext context)
        {
            if (GetIgnoreUrlsReg(_jwtConfig.IgnoreUrls).Match(context.Request.Path).Success)
            {
                return Validate(context, false);
            }
            else
            {
                return Validate(context, true);
            }
        }
        private Task Validate(HttpContext context, bool ValidateToken)
        {
            //验证token的情况下执行
            if (ValidateToken && context.Request.Headers.TryGetValue(_jwtConfig.HeadField, out Microsoft.Extensions.Primitives.StringValues authValue))
            {
                string authstr = authValue.ToString();
                if (_jwtConfig.Prefix.Length > 0)
                {
                    if (!authstr.Contains(_jwtConfig.Prefix))
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("{\"Status\":401,\"StatusMsg\":\"权限验证失败\"}");
                    }
                    authstr = authValue.ToString()[_jwtConfig.Prefix.Length..];
                }
                if (_jwt.ValidateToken(authstr, out Dictionary<string, object> Clims))
                {
                    List<string> climsKeys = new() { "nbf", "exp", "iat", "iss", "aud" };
                    IDictionary<string, object> RenewalDic = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, object> item in Clims)
                    {
                        if (climsKeys.FirstOrDefault(o => o == item.Key) == null)
                        {
                            if (context.Items.Keys.Contains(item.Key))//如果存在key则清理key;
                            {
                                context.Items.Remove(item.Key);
                            }
                            context.Items.Add(item.Key, item.Value);
                            RenewalDic.Add(item.Key, item.Value);
                        }
                    }
                    //验证通过的情况下判断续期时间
                    if (Clims.Keys.FirstOrDefault(o => o == "exp") != null)
                    {
                        DateTime start = new DateTime(1970, 1, 1, 0, 0, 0);
                        long timespanStart = long.Parse(Clims["nbf"].ToString());//token有效时间的开始时间点
                        DateTime tartDate = start.AddSeconds(timespanStart).ToLocalTime();
                        TimeSpan o = DateTime.Now - tartDate;//当前时间减去开始时间大于续期时间限制
                        if (o.TotalMinutes > _jwtConfig.RenewalTime)
                        {
                            //执行续期
                            string newToken = _jwt.GetToken(RenewalDic);
                            context.Response.Headers.Add(_jwtConfig.ReTokenHeadField, newToken);
                        }
                    }
                    return _next(context);
                }
                else
                {
                    if (ValidateToken == true)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("{\"Status\":401,\"StatusMsg\":\"权限验证失败\"}");
                    }
                    else
                    {
                        return _next(context);
                    }
                }
            }
            else
            {
                if (ValidateToken == true)
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync("{\"Status\":401,\"StatusMsg\":\"权限验证失败\"}");
                }
                else
                {
                    //不验证Token的情况下,如果有token字段,读取信息
                    context.Request.Headers.TryGetValue(_jwtConfig.HeadField, out Microsoft.Extensions.Primitives.StringValues authValue1);
                    string authstr = authValue1.ToString();
                    if (_jwtConfig.Prefix.Length > 0)
                    {
                        if (!authstr.Contains(_jwtConfig.Prefix))
                        {
                            return _next(context);//这里直接跳过
                        }
                        authstr = authValue1.ToString()[_jwtConfig.Prefix.Length..];
                    }
                    if (_jwt.ValidateToken(authstr, out Dictionary<string, object> Clims))
                    {
                        List<string> climsKeys = new() { "nbf", "exp", "iat", "iss", "aud" };
                        IDictionary<string, object> RenewalDic = new Dictionary<string, object>();
                        foreach (KeyValuePair<string, object> item in Clims)
                        {
                            if (climsKeys.FirstOrDefault(o => o == item.Key) == null)
                            {
                                if (context.Items.Keys.Contains(item.Key))//如果存在key则清理key;
                                {
                                    context.Items.Remove(item.Key);
                                }
                                context.Items.Add(item.Key, item.Value);
                                RenewalDic.Add(item.Key, item.Value);
                            }
                        }
                        //验证通过的情况下判断续期时间
                        if (Clims.Keys.FirstOrDefault(o => o == "exp") != null)
                        {
                            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0);
                            long timespanStart = long.Parse(Clims["nbf"].ToString());//token有效时间的开始时间点
                            DateTime tartDate = start.AddSeconds(timespanStart).ToLocalTime();
                            TimeSpan o = DateTime.Now - tartDate;//当前时间减去开始时间大于续期时间限制
                            if (o.TotalMinutes > _jwtConfig.RenewalTime)
                            {
                                //执行续期
                                string newToken = _jwt.GetToken(RenewalDic);
                                context.Response.Headers.Add(_jwtConfig.ReTokenHeadField, newToken);
                            }
                        }
                    }
                    return _next(context);
                }
            }
        }
    }

    /// <summary>
    /// 扩展方法，将中间件暴露出去
    /// </summary>
    public static class UseUseJwtMiddlewareExtensions
    {
        /// <summary>
        /// 权限检查
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseJwt(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UseJwtMiddleware>();
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace SuperTerminal.Data.SqlSugarContent
{
    public static class SqlsugarSetup
    {
        /// <summary>
        /// 注入SqlSugar
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddSqlsugarSetup(this IServiceCollection services, IConfiguration configuration)
        {
            SqlSugarScope sqlSugar = new(new ConnectionConfig()
            {
                DbType = DbType.MySql,
                ConnectionString = configuration.GetConnectionString("MySqlConnectionString"),
                IsAutoCloseConnection = true,
                LanguageType = LanguageType.Chinese
            }
            ,
            db =>
            {
                db.Aop.OnError = (obj) =>
                {
                    Console.WriteLine(obj.Message);
                };
                db.Aop.OnLogExecuted = (sql, pars) =>
                {
                    Console.WriteLine(sql);//输出sql
                    Console.WriteLine(JsonSerializer.Serialize(pars.Select(item => new { item.ParameterName, item.Value }).ToList()));
                };
                //过滤已经假删除的数据
                Type[] types = Assembly.Load("SuperTerminal.Data").GetTypes().Where(o => (typeof(IModel)).IsAssignableFrom(o)).ToArray();
                foreach (Type entityType in types)
                {
                    LambdaExpression lambda = DynamicExpressionParser.ParseLambda
                        (new[] { Expression.Parameter(entityType, "it") },
                         typeof(bool), $"it.IsDeleted ==false",
                          false);
                    db.QueryFilter.Add(new TableFilterItem<object>(entityType, lambda)); //将Lambda传入过滤器
                }
            }
            );
            services.AddSingleton<ISqlSugarClient>(sqlSugar);
        }
    }
}

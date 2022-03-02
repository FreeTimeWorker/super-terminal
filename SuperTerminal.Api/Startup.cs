using AutoMapper;
using CSRedis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SuperTerminal.Data.Maintain;
using SuperTerminal.Data.SqlSugarContent;
using SuperTerminal.GlobalService;
using SuperTerminal.JWT;
using SuperTerminal.MiddleWare;
using SuperTerminal.Utity;
using System;

namespace SuperTerminal.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            RedisHelper.Initialization(new CSRedisClient(Configuration["Redis:Connection"]));
        }
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddSqlsugarSetup(Configuration);//单例注入数据库上下文
            services.AddScoped<IDbContext, SqlSugarContext>();//注入上下文,常用的数据库操作
            services.AddSingleton(sp => new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperConfiguration>()).CreateMapper());//AutoMap配置
            services.AddDbContext<MaintainContent>(options => options.UseMySql(Configuration.GetConnectionString("MySqlConnectionString"), MySqlServerVersion.LatestSupportedServerVersion));//这里需要Mysql版本号
            services.AddTransient<IJwt, Jwt>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();//以一种简化的方式访问httpcontext
            services.AddTransient<IHttpParameter, HttpParameter>();
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                //不更改元数据的key的大小写   
                options.JsonSerializerOptions.PropertyNamingPolicy = null;//属性名称序列化为其他形式，null为不转换原样输出
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;//是否区分大小写的比较，false为比较，true为不比较
                options.JsonSerializerOptions.MaxDepth = 1024;//对象循环深度，默认32 
                options.JsonSerializerOptions.WriteIndented = true;//输出json是否带格式，默认不携带任何格式
                options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());//时间输入格式为yyyy-MM-dd，时间序列化为yyyy-MM-dd
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("SuperTerminal", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Name = "Token",
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入SuperTerminal {token}（注意两者之间是一个空格）",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     { new OpenApiSecurityScheme
                     {
                          Reference = new OpenApiReference()
                          {
                              Id = "SuperTerminal",
                              Type = ReferenceType.SecurityScheme
                          }
                     }, Array.Empty<string>() }
                 });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SuperTerminal.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ServiceAgent.Provider = app.ApplicationServices;//DI对象处理器
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SuperTerminal.Api v1"));
            }
            app.UseRouting();
            app.UseJwt();//加入jwt
            app.UseHttpParamter();//jwt之后才有userid,先后顺序要弄对
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

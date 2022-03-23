using AutoMapper;
using CSRedis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SuperTerminal.Data.Maintain;
using SuperTerminal.Data.SqlSugarContent;
using SuperTerminal.GlobalService;
using SuperTerminal.JWT;
using SuperTerminal.MessageHandler.Instant;
using SuperTerminal.MiddleWare;
using SuperTerminal.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            services.AddSqlsugarSetup(Configuration);//����ע�����ݿ�������
            services.AddScoped<IDbContext, SqlSugarContext>();//ע��������,���õ����ݿ����
            services.AddSingleton(sp => new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperConfiguration>()).CreateMapper());//AutoMap����
            services.AddDbContext<MaintainContent>(options => options.UseMySql(Configuration.GetConnectionString("MySqlConnectionString"), MySqlServerVersion.LatestSupportedServerVersion));//������ҪMysql�汾��
            services.AddTransient<IJwt, Jwt>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();//��һ�ּ򻯵ķ�ʽ����httpcontext
            services.AddTransient<IHttpParameter, HttpParameter>();
            services.AddHttpClient();//OsHelper��ʹ����IHttpClientFactory Ҫ��OsHelperע��ǰע��
            services.AddSingleton<OsHelper>();
            services.AddSignalR();
            //ע��service
            Dictionary<Type, Type[]> types = GetTypes("SuperTerminal.Service");
            foreach (KeyValuePair<Type, Type[]> item in types)
            {
                foreach (Type typeArray in item.Value)
                {
                    services.AddTransient(typeArray, item.Key);
                }
            }
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                //������Ԫ���ݵ�key�Ĵ�Сд   
                options.JsonSerializerOptions.PropertyNamingPolicy = null;//�����������л�Ϊ������ʽ��nullΪ��ת��ԭ�����
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;//�Ƿ����ִ�Сд�ıȽϣ�falseΪ�Ƚϣ�trueΪ���Ƚ�
                options.JsonSerializerOptions.MaxDepth = 1024;//����ѭ����ȣ�Ĭ��32 
                options.JsonSerializerOptions.WriteIndented = true;//���json�Ƿ����ʽ��Ĭ�ϲ�Я���κθ�ʽ
                options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());//ʱ�������ʽΪyyyy-MM-dd��ʱ�����л�Ϊyyyy-MM-dd
            });
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("SuperTerminal", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Name = "Token",
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������SuperTerminal {token}��ע������֮����һ���ո�",
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
            ServiceAgent.Provider = app.ApplicationServices;//DI��������
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SuperTerminal.Api v1"));
            }
            app.UseRouting();
            app.UseJwt();//����jwt
            app.UseHttpParamter();//jwt֮�����userid,�Ⱥ�˳��ҪŪ��
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<InstantMessage>("/instantMessage"); //ÿ�����󶼻���jwt����������ļ�ʱͨѶ����Ҫͨ��Ȩ����֤�ġ�
                endpoints.MapControllers();
            });
        }
        /// <summary>
        /// ͨ�����򼯻�ȡ���е������Լ���Ӧ�Ľӿ�����ע��
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        private static Dictionary<Type, Type[]> GetTypes(string assemblyName)
        {
            if (!string.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> ts = assembly.GetTypes().ToList();
                Dictionary<Type, Type[]> result = new();
                foreach (Type item in ts.Where(s => !s.IsInterface))
                {
                    Type[] interfaceType = item.GetInterfaces();
                    if (item.IsGenericType || interfaceType.Length < 1)
                    {
                        continue;
                    }

                    result.Add(item, interfaceType);
                }
                return result;
            }
            return new Dictionary<Type, Type[]>();
        }
    }
}

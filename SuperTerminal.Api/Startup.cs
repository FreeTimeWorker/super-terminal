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
            
            services.AddSqlsugarSetup(Configuration);//����ע�����ݿ�������
            services.AddScoped<IDbContext, SqlSugarContext>();//ע��������,���õ����ݿ����
            services.AddSingleton(sp => new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperConfiguration>()).CreateMapper());//AutoMap����
            services.AddDbContext<MaintainContent>(options => options.UseMySql(Configuration.GetConnectionString("MySqlConnectionString"), MySqlServerVersion.LatestSupportedServerVersion));//������ҪMysql�汾��
            services.AddTransient<IJwt, Jwt>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();//��һ�ּ򻯵ķ�ʽ����httpcontext
            services.AddTransient<IHttpParameter, HttpParameter>();
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                //������Ԫ���ݵ�key�Ĵ�Сд   
                options.JsonSerializerOptions.PropertyNamingPolicy = null;//�����������л�Ϊ������ʽ��nullΪ��ת��ԭ�����
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;//�Ƿ����ִ�Сд�ıȽϣ�falseΪ�Ƚϣ�trueΪ���Ƚ�
                options.JsonSerializerOptions.MaxDepth = 1024;//����ѭ����ȣ�Ĭ��32 
                options.JsonSerializerOptions.WriteIndented = true;//���json�Ƿ����ʽ��Ĭ�ϲ�Я���κθ�ʽ
                options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());//ʱ�������ʽΪyyyy-MM-dd��ʱ�����л�Ϊyyyy-MM-dd
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);
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
                endpoints.MapControllers();
            });
        }
    }
}

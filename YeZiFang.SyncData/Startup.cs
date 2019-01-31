using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Shitou.Framework.Log4net;
using Shitou.Framework.ORM;
using Swashbuckle.AspNetCore.Swagger;
using YeZiFang.SyncData.Service;

namespace YeZiFang.SyncData
{
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            //configure log4net
            env.ConfigureLog4Net("config/log4net.config");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //add log4net service
            services.AddLog4Net();
            //add mysql
            services.AddMySql(new MySqlConnection(Configuration.GetConnectionString("Mysql")));
            //add mongodb
            services.AddMongodb(Configuration.GetConnectionString("Mongo"));
            //add mvc
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            //add swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info
                {
                    Title = "yezi数据同步工具Api",
                    Version = "1.0",
                    Description = "YeZiFang.SyncData API - V1.0"
                    //TermsOfService = "None",
                    //Contact = new Contact
                    //{
                    //    Name = "yezi",
                    //    Email = string.Empty,
                    //    Url = "https://www.yezi.com/"
                    //},
                    //License = new License
                    //{
                    //    Name = "yezi",
                    //    Url = "https://www.yezi.com/"
                    //}
                });
                config.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YeZiFang.SyncData.xml"), true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            //user Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "YeZiFang.SyncData API-v1");
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Serialization;
using Shitou.Framework.Log4net;
using Shitou.Framework.ORM;
using Swashbuckle.AspNetCore.Swagger;
using YeZiFang.Application.Extensions;
using YeZiFang.Application.Filter;
using YeZiFang.Consul;

namespace YeZiFang.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddConsulFile("config/consulsettings.json");
            //configure log4net
            env.ConfigureLog4Net("config/log4net.config");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //add mvc by options
            services.AddMvc(options =>
            {
                //全局请求处理
                options.Filters.Add(typeof(GlobalActionFilterAttribute));
                //全局异常处理
                options.Filters.Add(typeof(GlobalExceptionFilterAttribute));
                //权限验证过滤
                //options.Filters.Add(typeof(AuthFilterAttribute));
            })
            // add Json Format
            .AddJsonOptions(options =>
            {
                //对 JSON 数据使用混合大小写。跟属性名同样的大小写输出
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //对 JSON 数据使用混合大小写。驼峰式, 适用于javascript 首字母小写形式.
                //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //add log4net service
            services.AddLog4Net();
            //add mysql
            services.AddMySql(new MySqlConnection(Configuration.GetConnectionString("Mysql")));
            //add bussiness service
            services.AddBusinessService();
            //add consul
            services.AddConsul();
            //add swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info
                {
                    Title = "YeZiFang.Api数据接口",
                    Version = "1.0",
                    Description = "YeZiFang.Api - V1.0"
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
                config.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YeZiFang.Api.xml"), true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //use mvc
            app.UseMvc();
            //user consul
            app.UseConsul();
            //use Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "YeZiFang.Api-v1");
            });
        }

        /// <summary>
        /// 本地ip
        /// </summary>
        private static string LocalIPAddress
        {
            get
            {
                UnicastIPAddressInformation mostSuitableIp = null;
                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                foreach (var network in networkInterfaces)
                {
                    if (network.OperationalStatus != OperationalStatus.Up)
                        continue;
                    var properties = network.GetIPProperties();
                    if (properties.GatewayAddresses.Count == 0)
                        continue;

                    foreach (var address in properties.UnicastAddresses)
                    {
                        if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                            continue;
                        if (IPAddress.IsLoopback(address.Address))
                            continue;
                        return address.Address.ToString();
                    }
                }
                return mostSuitableIp != null
                    ? mostSuitableIp.Address.ToString()
                    : "";
            }
        }
    }
}

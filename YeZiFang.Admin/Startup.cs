using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Serialization;
using Shitou.Framework.Log4net;
using Shitou.Framework.ORM;
using YeZiFang.Application.Extensions;
using YeZiFang.Application.Filter;
using YeZiFang.Application.Middleware;

namespace YeZiFang.Admin
{
    public class Startup
    {
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

            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //添加认证Cookie信息
            services.AddAuthentication(options =>
            {
                //DefaultSignInScheme, DefaultSignOutScheme, DefaultChallengeScheme, DefaultForbidScheme 等都会使用该 Scheme 作为默认值。
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            //用来注册 CookieAuthenticationHandler，由它来完成身份认证的主要逻辑。
            .AddCookie(options =>
            {
                // 在这里可以根据需要添加一些Cookie认证相关的配置，在本次示例中使用默认值就可以了。
                options.LoginPath = "/Login";
            });
            //mvc action上下文，在taghelper(LayuiPagerTagHelper.cs)扩展里用到
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //add session service
            services.AddSession(options =>
            {
                //设置过期时间为20分钟
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });

            //add log4net service
            services.AddLog4Net();
            //add mysql
            services.AddMySql(new MySqlConnection(Configuration.GetConnectionString("Mysql")));
            //add bussiness service
            services.AddBusinessService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Home/NoFound");
            //静态文件
            app.UseStaticFiles();
            //启用Session
            app.UseSession();
            //设置根目录下'Upload'文件可静态访问
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), @"Upload");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(uploadPath),
                RequestPath = new PathString("/Upload")
            });
            //验证中间件
            app.UseAuthentication();
            app.UseAuth();
            //use mvc
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

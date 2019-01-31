
using YeZiFang.Dao;
using YeZiFang.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Data;


namespace YeZiFang.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register The Service Layer
        /// </summary>
        /// <param name="services"></param>
        public static void AddBusinessService(this IServiceCollection services)
        {
            //sevice
            services.AddTransient<IBaseService, BaseService>();
            services.AddTransient<ISystemService, SystemService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<ICommonService, CommonService>();

            //dao
            services.AddTransient<ISystemDao, SystemDao>();
            services.AddTransient<IProjectDao, ProjectDao>();
            services.AddTransient<ICommonDao, CommonDao>();
        }
    }
}

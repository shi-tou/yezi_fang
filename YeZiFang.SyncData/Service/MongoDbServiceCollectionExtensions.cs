using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YeZiFang.SyncData.Service
{
    public static class MongoDbServiceCollectionExtensions
    {
        /// <summary>
        /// 注册mongodb服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongodb(this IServiceCollection services, string connectionString)
        {
            MongoConfig mongoConfig = new MongoConfig(connectionString);
            services.AddTransient(typeof(IMongoService), _ => new MongoService(mongoConfig));
            return services;
        }
        /// <summary>
        /// 注册mongodb服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="typeService"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongodb(this IServiceCollection services, string connectionString, Type typeService = null)
        {
            if (typeService == null)
                typeService = typeof(IMongoService);
            MongoConfig mongoConfig = new MongoConfig(connectionString);
            services.AddTransient(typeService, _ => new MongoService(mongoConfig));
            return services;
        }
    }
}

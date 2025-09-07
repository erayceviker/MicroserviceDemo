using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroserviceDemo.Catalog.Api.Options;
using MongoDB.Driver;

namespace MicroserviceDemo.Catalog.Api.Repositories
{
    public static class RepositoryExt
    {
        public static IServiceCollection AddRepositoryExtension(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var options = sp.GetRequiredService<MongoOption>();
                return new MongoClient(options.ConnectionString);
            });

            services.AddScoped<AppDbContext>(sp =>
            {
                var options = sp.GetRequiredService<MongoOption>();
                var client = sp.GetRequiredService<IMongoClient>();
                var database = client.GetDatabase(options.DatabaseName);
                return AppDbContext.Create(database);
            });


            return services;
        }
    }
}
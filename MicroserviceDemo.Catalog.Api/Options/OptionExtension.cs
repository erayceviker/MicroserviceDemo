using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MicroserviceDemo.Catalog.Api.Options
{
    public static class OptionExtension
    {
        public static IServiceCollection AddOptionsExtension(this IServiceCollection services)
        {
            services.AddOptions<MongoOption>()
                .BindConfiguration(nameof(MongoOption))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddSingleton<MongoOption>(sp => sp.GetRequiredService<IOptions<MongoOption>>().Value);

            return services;
        }
    }
}
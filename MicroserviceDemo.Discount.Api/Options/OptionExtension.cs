using Microsoft.Extensions.Options;

namespace MicroserviceDemo.Discount.Api.Options
{
    public static class OptionExtension
    {
        public static IServiceCollection AddOptionsExt(this IServiceCollection services)
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
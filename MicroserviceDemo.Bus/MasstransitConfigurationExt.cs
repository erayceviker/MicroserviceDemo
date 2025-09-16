using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceDemo.Bus
{
    public static class MasstransitConfigurationExt
    {
        public static IServiceCollection AddCommonMasstransitExt(this IServiceCollection services,
            IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;


            services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });


                    cfg.ConfigureEndpoints(ctx);

                });
            });


            return services;
        }
    }
}

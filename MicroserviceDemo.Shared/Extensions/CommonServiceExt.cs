using FluentValidation;
using FluentValidation.AspNetCore;
using MicroserviceDemo.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceDemo.Shared.Extensions
{
    public static class CommonServiceExt
    {
        public static IServiceCollection AddCommonServiceExt(this IServiceCollection services, Type assembly)
        {
            services.AddHttpContextAccessor();
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(assembly);
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddAutoMapper(cfg =>
            {
            }, profileAssemblyMarkerTypes:assembly);

            return services;
        }
    }
}

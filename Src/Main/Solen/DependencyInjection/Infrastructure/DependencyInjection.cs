using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Solen.DependencyInjection.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IMvcBuilder mvcBuilder,
            IConfiguration configuration)
        {
            services.AddAppAuthentication(configuration)
                .AddSwagger()
                .AddMediatR(mvcBuilder)
                .AddPersistence(configuration)
                .AddIdentity()
                .AddNotifications(configuration)
                .AddResourcesStorage(configuration)
                .AddNotifications(configuration);

            return services;
        }
    }
}
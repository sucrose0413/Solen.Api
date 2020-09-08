using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Solen.DependencyInjection.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationCommon(this IServiceCollection services)
        {
            services.AddSubscription().AddResources().AddNotifications().AddSecurity();

            return services;
        }
    }
}
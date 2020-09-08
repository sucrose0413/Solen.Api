using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.Common.Subscription;
using Solen.Core.Application.Common.Subscription.Impl;
using Solen.Persistence.Common.Subscription;

namespace Solen.DependencyInjection.Common
{
    public static class Subscription
    {
        public static IServiceCollection AddSubscription(this IServiceCollection services)
        {
            services.AddScoped<IOrganizationSubscriptionManager, OrganizationSubscriptionManager>();
            services.AddScoped<IOrganizationSubscriptionRepository, OrganizationSubscriptionRepository>();


            return services;
        }
    }
}
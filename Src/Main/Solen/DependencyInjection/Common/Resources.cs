using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.Common.Resources;
using Solen.Core.Application.Common.Resources.Impl;
using Solen.Persistence.Common.Resources;
using Solen.WebApi;

namespace Solen.DependencyInjection.Common
{
    public static class Resources
    {
        public static IServiceCollection AddResources(this IServiceCollection services)
        {
            services.AddScoped<IResourceFile, ResourceFile>();

            services.AddScoped<IAppResourceManager, AppResourceManager>();
            services.AddScoped<IAppResourceManagerRepo, AppResourceManagerRepo>();
            
            return services;
        }
    }
}
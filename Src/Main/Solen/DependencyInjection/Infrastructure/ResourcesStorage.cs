using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Solen.Core.Application.Common.Resources.Impl;
using Solen.Infrastructure.Resources;
using Solen.Infrastructure.Resources.LocalStorage;
using ResourceAccessor = Solen.Core.Application.Common.Resources.ResourceAccessor;

namespace Solen.DependencyInjection.Infrastructure
{
    public static class ResourcesStorage
    {
        public static IServiceCollection AddResourcesStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IResourceStorageManagerFactory, ResourceStorageManagerFactory>(ResourceAccessorsFactory());
            
            // Local storage
            services.Configure<LocalStorageSettings>(configuration.GetSection("LocalStorageSettings"));

            return services;
        }
        
        private static Func<IServiceProvider, ResourceStorageManagerFactory> ResourceAccessorsFactory()
        {
            return provider =>
            {
                var config = provider.GetService<IOptions<LocalStorageSettings>>();

                var resourceAccessors = new List<ResourceAccessor>
                {
                    new LocalImageAccessor(config),
                    new LocalRawAccessor(config),
                    new LocalVideoAccessor(config)
                };
                
                return new ResourceStorageManagerFactory(resourceAccessors);

            };
        }
    }
}
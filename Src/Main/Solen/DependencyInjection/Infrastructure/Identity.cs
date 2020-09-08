using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Identity.Impl;
using Solen.Persistence.Common.Identity;


namespace Solen.DependencyInjection.Infrastructure
{
    public static class Identity
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IUserManagerRepo, UserManagerRepo>();
            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<IRoleManagerRepo, RoleManagerRepo>();

            return services;
        }
    }
}
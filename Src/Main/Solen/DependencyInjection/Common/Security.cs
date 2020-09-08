using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.Common.Security;
using Solen.Infrastructure.Security;

namespace Solen.DependencyInjection.Common
{
    public static class Security
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            services.AddScoped<ISecurityConfig, SecurityConfig>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IPasswordHashGenerator, PasswordHashGenerator>();
            services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
            services.AddScoped<IRandomTokenGenerator, RandomTokenGenerator>();
            services.AddTransient<IDateTime, MachineDateTime>();
            
            return services;
        }
    }
}
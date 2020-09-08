using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.UnitOfWork;
using Solen.Persistence;
using Solen.Persistence.UnitOfWork;

namespace Solen.DependencyInjection.Infrastructure
{
    public static class Persistence
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // Db context
            services.AddDbContext<SolenDbContext>(x => x.UseMySql(configuration.GetConnectionString("Default")));

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
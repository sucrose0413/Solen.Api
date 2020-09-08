using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.SigningUp.EventsHandlers.Organizations;
using Solen.Core.Application.SigningUp.Organizations.Commands;
using Solen.Core.Application.SigningUp.Organizations.Queries;
using Solen.Core.Application.SigningUp.Services.Organizations;
using Solen.Core.Application.SigningUp.Services.Organizations.Queries;
using Solen.Core.Application.SigningUp.Services.Users.Commands;
using Solen.Core.Application.SigningUp.Services.Users.Queries;
using Solen.Core.Application.SigningUp.Users.Commands;
using Solen.Core.Application.SigningUp.Users.Queries;
using Solen.Persistence.SigningUp.Organizations;
using Solen.Persistence.SigningUp.Organizations.Queries;
using Solen.Persistence.SigningUp.Users.Queries;
using Solen.WebApi.SigningUp;

namespace Solen.DependencyInjection.Application
{
    public static class SigningUp
    {
        public static IServiceCollection AddSigninUp(this IServiceCollection services, IMvcBuilder mvcBuilder,
            IConfiguration configuration)
        {
            // Controllers 
            mvcBuilder.AddApplicationPart(typeof(OrganizationsController).GetTypeInfo().Assembly);


            //------------------------- Commands
            // InitOrganizationSigningUp
            services.AddScoped<IInitOrganizationSigningUpService, InitOrganizationSigningUpService>();
            services.AddScoped<IInitOrganizationSigningUpRepository, InitOrganizationSigningUpRepository>();
            // CompleteOrganizationSigningUp
            services.Configure<CompleteOrganizationSigningUpPageInfo>(
                configuration.GetSection("CompleteOrganizationSigningUpPageInfo"));
            services.AddScoped<ICompleteOrganizationSigningUpService, CompleteOrganizationSigningUpService>();
            services.AddScoped<ICompleteOrganizationSigningUpRepository, CompleteOrganizationSigningUpRepository>();
            // CompleteUserSigningUp
            services.AddScoped<ICompleteUserSigningUpService, CompleteUserSigningUpService>();

            //------------------------- Queries
            // CheckOrganizationSigningUpToken
            services.AddScoped<ICheckOrganizationSigningUpTokenService, CheckOrganizationSigningUpTokenService>();
            services.AddScoped<ICheckOrganizationSigningUpTokenRepository, CheckOrganizationSigningUpTokenRepository>();
            // CheckUserSigningUpToken
            services.AddScoped<ICheckUserSigningUpTokenService, CheckUserSigningUpTokenService>();
            services.AddScoped<ICheckUserSigningUpTokenRepository, CheckUserSigningUpTokenRepository>();

            return services;
        }
    }
}
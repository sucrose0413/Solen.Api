using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.Settings.Organization.Commands;
using Solen.Core.Application.Settings.Organization.Queries;
using Solen.Core.Application.Settings.Organization.Services.Commands;
using Solen.Core.Application.Settings.Organization.Services.Queries;
using Solen.Persistence.Settings.Organization.Commands;
using Solen.Persistence.Settings.Organization.Queries;
using Solen.WebApi.Settings.Organization;

namespace Solen.DependencyInjection.Application
{
    public static class OrganizationSettings
    {
        public static IServiceCollection AddOrganizationSettings(this IServiceCollection services,
            IMvcBuilder mvcBuilder)
        {
            // Controllers 
            mvcBuilder.AddApplicationPart(typeof(OrganizationController).GetTypeInfo().Assembly);

            //------------------------- Queries
            // GetOrganizationInfoTemplates
            services.AddScoped<IGetOrganizationInfoService, GetOrganizationInfoService>();
            services.AddScoped<IGetOrganizationInfoRepository, GetOrganizationInfoRepository>();


            //------------------------- Commands
            // UpdateOrganizationInfo
            services.AddScoped<IUpdateOrganizationInfoService, UpdateOrganizationInfoService>();
            services.AddScoped<IUpdateOrganizationInfoRepository, UpdateOrganizationInfoRepository>();

            return services;
        }
    }
}
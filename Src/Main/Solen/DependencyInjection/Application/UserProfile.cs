using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.UserProfile.Commands;
using Solen.Core.Application.UserProfile.Queries;
using Solen.Core.Application.UserProfile.Services.Commands;
using Solen.Core.Application.UserProfile.Services.Queries;
using Solen.WebApi.UserProfile;

namespace Solen.DependencyInjection.Application
{
    public static class UserProfile
    {
        public static IServiceCollection AddUserProfile(this IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            // Controllers 
            mvcBuilder.AddApplicationPart(typeof(UserProfileController).GetTypeInfo().Assembly);

            //------------------------- Queries
            // GetCurrentUserInfo
            services.AddScoped<IGetCurrentUserInfoService, GetCurrentUserInfoService>();

            //------------------------- Commands
            // UpdateCurrentUserInfo
            services.AddScoped<IUpdateCurrentUserInfoService, UpdateCurrentUserInfoService>();
            // UpdateCurrentUserPassword
            services.AddScoped<IUpdateCurrentUserPasswordService, UpdateCurrentUserPasswordService>();

            return services;
        }
    }
}
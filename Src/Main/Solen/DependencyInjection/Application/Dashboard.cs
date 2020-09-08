using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.Dashboard.Queries;
using Solen.Core.Application.Dashboard.Services.Queries;
using Solen.Persistence.Dashboard.Queries;
using Solen.WebApi.Dashboard;


namespace Solen.DependencyInjection.Application
{
    public static class Dashboard
    {
        public static IServiceCollection AddDashboard(this IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            // Controllers 
            mvcBuilder.AddApplicationPart(typeof(CoursesInfoController).GetTypeInfo().Assembly);

            //------------------------- Queries
            // GetCoursesInfo
            services.AddScoped<IGetCoursesInfoService, GetCoursesInfoService>();
            services.AddScoped<IGetCoursesInfoRepository, GetCoursesInfoRepository>();
            // GetLearningPathsInfo
            services.AddScoped<IGetLearningPathsInfoService, GetLearningPathsInfoService>();
            services.AddScoped<IGetLearningPathsInfoRepository, GetLearningPathsInfoRepository>();
            // GetStorageInfo
            services.AddScoped<IGetStorageInfoService, GetStorageInfoService>();
            // GetUserCountInfo
            services.AddScoped<IGetUserCountInfoService, GetUserCountInfoService>();
            // GetLearnerInfo
            services.AddScoped<IGetLearnerInfoService, GetLearnerInfoService>();
            services.AddScoped<IGetLearnerInfoRepository, GetLearnerInfoRepository>();


            return services;
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Solen.DependencyInjection.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationCore(this IServiceCollection services,
            IMvcBuilder mvcBuilder, IConfiguration configuration)
        {
            services.AddApplicationAuth(mvcBuilder, configuration)
                .AddCoursesManagement(mvcBuilder)
                .AddCoursesManagementEdit(mvcBuilder)
                .AddCoursesManagementEventsHandlers()
                .AddCoursesManagementCommon()
                .AddLearning(mvcBuilder)
                .AddUsersManagement(mvcBuilder, configuration)
                .AddNotifications(mvcBuilder)
                .AddLearningPaths(mvcBuilder)
                .AddSigninUp(mvcBuilder, configuration)
                .AddNotificationsSettings(mvcBuilder)
                .AddOrganizationSettings(mvcBuilder)
                .AddUserProfile(mvcBuilder)
                .AddDashboard(mvcBuilder);

            return services;
        }
    }
}
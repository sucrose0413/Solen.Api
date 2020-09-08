using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.CoursesManagement.EventsHandlers.Courses;
using Solen.Core.Application.CoursesManagement.EventsHandlers.Lectures;
using Solen.Core.Application.CoursesManagement.EventsHandlers.Modules;
using Solen.Persistence.CoursesManagement.EventsHandlers.Courses;
using Solen.Persistence.CoursesManagement.EventsHandlers.Lectures;
using Solen.Persistence.CoursesManagement.EventsHandlers.Modules;

namespace Solen.DependencyInjection.Application
{
    public static class CoursesManagementEventsHandlers
    {
        public static IServiceCollection AddCoursesManagementEventsHandlers(this IServiceCollection services)
        {
            services
                .AddScoped<ISendNotificationsToCourseLearnersRepo, SendNotificationsToCourseLearnersRepo>();

            services.AddScoped<ILectureResourcesRepo, LectureResourcesRepo>();
            services.AddScoped<IModuleResourcesRepo, ModuleResourcesRepo>();
            services.AddScoped<ICourseResourcesRepo, CourseResourcesRepo>();
            
            return services;
        }
    }
}
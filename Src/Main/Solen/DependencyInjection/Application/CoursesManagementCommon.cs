using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.CoursesManagement.Common;
using Solen.Core.Application.CoursesManagement.Common.Impl;
using Solen.Persistence.CoursesManagement.Common;

namespace Solen.DependencyInjection.Application
{
    public static class CoursesManagementCommon
    {
        public static IServiceCollection AddCoursesManagementCommon(this IServiceCollection services)
        {
            // CourseErrorsManager
            services.AddScoped<ICourseErrorsManager, CourseErrorsManager>(CourseErrorsFactory());
            services.AddScoped<INoModuleErrorRepository, CourseErrorsRepository>();
            services.AddScoped<INoLectureErrorsRepository, CourseErrorsRepository>();
            services.AddScoped<INoContentErrorsRepository, CourseErrorsRepository>();
            services.AddScoped<INoMediaErrorsRepository, CourseErrorsRepository>();

            return services;
        }
        
        private static Func<IServiceProvider, CourseErrorsManager> CourseErrorsFactory()
        {
            return serviceProvider =>
            {
                var noModuleErrorRepository = serviceProvider.GetService<INoModuleErrorRepository>();
                var noLectureErrorsRepository = serviceProvider.GetService<INoLectureErrorsRepository>();
                var noContentErrorsRepository = serviceProvider.GetService<INoContentErrorsRepository>();
                var noMediaErrorsRepository = serviceProvider.GetService<INoMediaErrorsRepository>();
                
                var courseErrorsHandlers = new List<ICourseErrors>
                {
                    new NoModuleErrorHandler(noModuleErrorRepository),
                    new NoLectureErrorsHandler(noLectureErrorsRepository),
                    new NoContentErrorsHandler(noContentErrorsRepository),
                    new NoMediaErrorsHandler(noMediaErrorsRepository)
                };
                
                return new CourseErrorsManager(courseErrorsHandlers);
            };
        }
    }
}
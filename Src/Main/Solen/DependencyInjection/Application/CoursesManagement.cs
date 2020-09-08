using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.CoursesManagement.Courses.Queries;
using Solen.Core.Application.CoursesManagement.LearningPaths.Queries;
using Solen.Core.Application.CoursesManagement.Lectures.Queries;
using Solen.Core.Application.CoursesManagement.Modules.Queries;
using Solen.Core.Application.CoursesManagement.Services.Courses;
using Solen.Core.Application.CoursesManagement.Services.LearningPaths;
using Solen.Core.Application.CoursesManagement.Services.Lectures;
using Solen.Core.Application.CoursesManagement.Services.Modules;
using Solen.Persistence.CoursesManagement.Courses;
using Solen.Persistence.CoursesManagement.LearningPaths;
using Solen.Persistence.CoursesManagement.Lectures;
using Solen.Persistence.CoursesManagement.Modules;
using Solen.WebApi.CoursesManagement;

namespace Solen.DependencyInjection.Application
{
    public static class CoursesManagement
    {
        public static IServiceCollection AddCoursesManagement(this IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            // Controllers 
            mvcBuilder.AddApplicationPart(typeof(CoursesController).GetTypeInfo().Assembly);

            //------------------------- Courses
            // GetCourse
            services.AddScoped<IGetCourseService, GetCourseService>();
            services.AddScoped<IGetCourseRepository, GetCourseRepository>();
            // GetCourseContent
            services.AddScoped<IGetCourseContentService, GetCourseContentService>();
            services.AddScoped<IGetCourseContentRepository, GetCourseContentRepository>();
            // GetCoursesList
            services.AddScoped<IGetCoursesListService, GetCoursesListService>();
            services.AddScoped<IGetCoursesListRepository, GetCoursesListRepository>();
            // GetCoursesFilters
            services.AddScoped<IGetCoursesFiltersService, GetCoursesFiltersService>();
            services.AddScoped<IGetCoursesFiltersRepository, GetCoursesFiltersRepository>();

            //------------------------- Learning Paths
            // GetCourseLearningPaths
            services.AddScoped<IGetCourseLearningPathsService, GetCourseLearningPathsService>();
            services.AddScoped<IGetCourseLearningPathsRepository, GetCourseLearningPathsRepository>();

            //------------------------- Lectures
            // GetLecture
            services.AddScoped<IGetLectureService, GetLectureService>();
            services.AddScoped<IGetLectureRepository, GetLectureRepository>();

            //------------------------- Modules
            // GetModule
            services.AddScoped<IGetModuleService, GetModuleService>();
            services.AddScoped<IGetModuleRepository, GetModuleRepository>();


            return services;
        }
    }
}
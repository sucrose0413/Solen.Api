using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.Learning.Commands;
using Solen.Core.Application.Learning.Queries;
using Solen.Core.Application.Learning.Services.Commands;
using Solen.Core.Application.Learning.Services.Queries;
using Solen.Persistence.Learning.Commands;
using Solen.Persistence.Learning.Queries;
using Solen.WebApi.Learning;

namespace Solen.DependencyInjection.Application
{
    public static class Learning
    {
        public static IServiceCollection AddLearning(this IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            // Controllers 
            mvcBuilder.AddApplicationPart(typeof(LearningController).GetTypeInfo().Assembly);


            //------------------------- Commands
            // AddLearnerAccessHistory
            services.AddScoped<IAddLearnerAccessHistoryService, AddLearnerAccessHistoryService>();
            services.AddScoped<IAddLearnerAccessHistoryRepository, AddLearnerAccessHistoryRepository>();
            // CompleteLecture
            services.AddScoped<ICompleteLectureService, CompleteLectureService>();
            services.AddScoped<ICompleteLectureRepository, CompleteLectureRepository>();
            // UncompleteLecture
            services.AddScoped<IUncompleteLectureService, UncompleteLectureService>();
            services.AddScoped<IUncompleteLectureRepository, UncompleteLectureRepository>();

            //------------------------- Queries
            // GetCourseContent
            services.AddScoped<IGetCourseContentService, GetCourseContentService>();
            services.AddScoped<IGetCourseContentRepository, GetCourseContentRepository>();
            // GetCourseOverview
            services.AddScoped<IGetCourseOverviewService, GetCourseOverviewService>();
            services.AddScoped<IGetCourseOverviewRepository, GetCourseOverviewRepository>();
            // GetCoursesList
            services.AddScoped<IGetCoursesListService, GetCoursesListService>();
            services.AddScoped<IGetCoursesListRepository, GetCoursesListRepository>();
            // GetCoursesFilters
            services.AddScoped<IGetCoursesFiltersService, GetCoursesFiltersService>();
            services.AddScoped<IGetCoursesFiltersRepository, GetCoursesFiltersRepository>();
            // GetCompletedLectures
            services.AddScoped<IGetCompletedLecturesService, GetCompletedLecturesService>();
            services.AddScoped<IGetCompletedLecturesRepository, GetCompletedLecturesRepository>();


            return services;
        }
    }
}
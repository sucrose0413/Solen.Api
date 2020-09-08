using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Application.LearningPaths.Queries;
using Solen.Core.Application.LearningPaths.Services.Commands;
using Solen.Core.Application.LearningPaths.Services.Queries;
using Solen.Persistence.LearningPaths.Commands;
using Solen.Persistence.LearningPaths.Queries;
using Solen.WebApi.LearningPaths;


namespace Solen.DependencyInjection.Application
{
    public static class LearningPaths
    {
        public static IServiceCollection AddLearningPaths(this IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            // Controllers 
            mvcBuilder.AddApplicationPart(typeof(LearningPathsController).GetTypeInfo().Assembly);

            //------------------------- Queries
            // GetLearningPathsList
            services.AddScoped<IGetLearningPathsService, GetLearningPathsService>();
            services.AddScoped<IGetLearningPathsRepository, GetLearningPathsRepository>();
            // GetLearningPath
            services.AddScoped<IGetLearningPathService, GetLearningPathService>();
            services.AddScoped<IGetLearningPathRepository, GetLearningPathRepository>();
            // GetLearningPathCourses
            services.AddScoped<IGetLearningPathCoursesService, GetLearningPathCoursesService>();
            services.AddScoped<IGetLearningPathCoursesRepository, GetLearningPathCoursesRepository>();
            // GetOtherCoursesToAdd
            services.AddScoped<IGetOtherCoursesToAddService, GetOtherCoursesToAddService>();
            services.AddScoped<IGetOtherCoursesToAddRepository, GetOtherCoursesToAddRepository>();
            // GetLearningPathLearners
            services.AddScoped<IGetLearningPathLearnersService, GetLearningPathLearnersService>();
            services.AddScoped<IGetLearningPathLearnersRepository, GetLearningPathLearnersRepository>();
            // GetLearnerProgress
            services.AddScoped<IGetLearnerProgressService, GetLearnerProgressService>();
            services.AddScoped<IGetLearnerProgressRepository, GetLearnerProgressRepository>();

            //------------------------- Commands
            // CreateLearningPath
            services.AddScoped<ICreateLearningPathService, CreateLearningPathService>();
            services.AddScoped<ICreateLearningPathRepository, CreateLearningPathRepository>();
            // DeleteLearningPath
            services.AddScoped<IDeleteLearningPathService, DeleteLearningPathService>();
            services.AddScoped<IDeleteLearningPathRepository, DeleteLearningPathRepository>();
            // UpdateLearningPath
            services.AddScoped<IUpdateLearningPathService, UpdateLearningPathService>();
            services.AddScoped<IUpdateLearningPathRepository, UpdateLearningPathRepository>();
            // AddCoursesToLearningPath
            services.AddScoped<IAddCoursesToLearningPathService, AddCoursesToLearningPathService>();
            services.AddScoped<IAddCoursesToLearningPathRepository, AddCoursesToLearningPathRepository>();
            // RemoveCourseFromLearningPath
            services.AddScoped<IRemoveCourseFromLearningPathService, RemoveCourseFromLearningPathService>();
            services.AddScoped<IRemoveCourseFromLearningPathRepository, RemoveCourseFromLearningPathRepository>();
            // UpdateCoursesOrders
            services.AddScoped<IUpdateCoursesOrdersService, UpdateCoursesOrdersService>();
            services.AddScoped<IUpdateCoursesOrdersRepository, UpdateCoursesOrdersRepository>();
            
            return services;
        }
    }
}
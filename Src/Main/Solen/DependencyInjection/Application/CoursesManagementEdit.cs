using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Application.CoursesManagement.Edit.Factories.LectureCreation;
using Solen.Core.Application.CoursesManagement.Edit.LearningPaths.Commands;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;
using Solen.Core.Application.CoursesManagement.Edit.Services.Courses;
using Solen.Core.Application.CoursesManagement.Edit.Services.LearningPaths;
using Solen.Core.Application.CoursesManagement.Edit.Services.Lectures;
using Solen.Core.Application.CoursesManagement.Edit.Services.Modules;
using Solen.Persistence.CoursesManagement.Edit.Courses;
using Solen.Persistence.CoursesManagement.Edit.LearningPaths;
using Solen.Persistence.CoursesManagement.Edit.Lectures;
using Solen.Persistence.CoursesManagement.Edit.Modules;
using Solen.WebApi.CoursesManagement.Edit;

namespace Solen.DependencyInjection.Application
{
    public static class CoursesManagementEdit
    {
        public static IServiceCollection AddCoursesManagementEdit(this IServiceCollection services,
            IMvcBuilder mvcBuilder)
        {
            // Controllers 
            mvcBuilder.AddApplicationPart(typeof(CoursesController).GetTypeInfo().Assembly);


            //------------------------- Courses
            // Common
            services.AddScoped<ICoursesCommonService, CoursesCommonService>();
            services.AddScoped<ICoursesCommonRepository, CoursesCommonRepository>();
            // CreateCourse
            services.AddScoped<ICreateCourseService, CreateCourseService>();
            services.AddScoped<ICreateCourseRepository, CreateCourseRepository>();
            // DeleteCourse
            services.AddScoped<IDeleteCourseService, DeleteCourseService>();
            services.AddScoped<IDeleteCourseRepository, DeleteCourseRepository>();
            // DraftService
            services.AddScoped<IDraftCourseService, DraftCourseService>();
            // PublishCourse
            services.AddScoped<IPublishCourseService, PublishCourseService>();
            services.AddScoped<IPublishCourseRepository, PublishCourseRepository>();
            // UnpublishCourse
            services.AddScoped<IUnpublishCourseService, UnpublishCourseService>();
            // UpdateCourse
            services.AddScoped<IUpdateCourseService, UpdateCourseService>();
            services.AddScoped<IUpdateCourseRepository, UpdateCourseRepository>();
            // UpdateModulesOrders
            services.AddScoped<IUpdateModulesOrdersService, UpdateModulesOrdersService>();
            services.AddScoped<IUpdateModulesOrdersRepository, UpdateModulesOrdersRepository>();

            //------------------------- Learning Paths
            // UpdateCourseLearningPaths
            services.AddScoped<IUpdateCourseLearningPathsService, UpdateCourseLearningPathsService>();
            services.AddScoped<IUpdateCourseLearningPathsRepository, UpdateCourseLearningPathsRepository>();
            
            //------------------------- Lectures
            // Common
            services.AddScoped<ILecturesCommonService, LecturesCommonService>();
            services.AddScoped<ILecturesCommonRepository, LecturesCommonRepository>();
            // CreateLecture
            services.AddScoped<ICreateLectureService, CreateLectureService>();
            services.AddScoped<ICreateLectureRepository, CreateLectureRepository>();
            services.AddScoped<ILectureCreatorFactory, LectureCreatorFactory>(LectureCreatorsFactory());
            // DeleteLecture
            services.AddScoped<IDeleteLectureService, DeleteLectureService>();
            services.AddScoped<IDeleteLectureRepository, DeleteLectureRepository>();
            // UpdateLecture
            services.AddScoped<IUpdateLectureService, UpdateLectureService>();
            services.AddScoped<IUpdateLectureRepository, UpdateLectureRepository>();
            // UploadMedia
            services.AddScoped<IUploadMediaService, UploadMediaService>();
            services.AddScoped<IUploadMediaRepository, UploadMediaRepository>();
            
            //------------------------- Modules
            // Common
            services.AddScoped<IModulesCommonService, ModulesCommonService>();
            services.AddScoped<IModulesCommonRepository, ModulesCommonRepository>();
            // CreateModule
            services.AddScoped<ICreateModuleService, CreateModuleService>();
            services.AddScoped<ICreateModuleRepository, CreateModuleRepository>();
            // DeleteModule
            services.AddScoped<IDeleteModuleService, DeleteModuleService>();
            services.AddScoped<IDeleteModuleRepository, DeleteModuleRepository>();
            // UpdateModule
            services.AddScoped<IUpdateModuleService, UpdateModuleService>();
            services.AddScoped<IUpdateModuleRepository, UpdateModuleRepository>();
            // UpdateLecturesOrders
            services.AddScoped<IUpdateLecturesOrdersService, UpdateLecturesOrdersService>();
            services.AddScoped<IUpdateLecturesOrdersRepository, UpdateLecturesOrdersRepository>();
            

            return services;
        }

        private static Func<IServiceProvider, LectureCreatorFactory> LectureCreatorsFactory()
        {
            return serviceProvider =>
            {
                var lectureCreators = new List<ILectureCreator> {new ArticleCreator(), new VideoCreator()};
                return new LectureCreatorFactory(lectureCreators);
            };
        }
    }
}
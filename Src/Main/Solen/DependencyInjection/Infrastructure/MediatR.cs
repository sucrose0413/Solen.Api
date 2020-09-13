using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using MicroElements.Swashbuckle.FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.Auth.EventsHandlers.PasswordTokenSet;
using Solen.Core.Application.Auth.Queries;
using Solen.Core.Application.CoursesManagement.Courses.Queries;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Application.CoursesManagement.EventsHandlers.Courses;
using Solen.Core.Application.Dashboard.Queries;
using Solen.Core.Application.Learning.Commands;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Application.Notifications.Queries;
using Solen.Core.Application.Settings.Notifications.Queries;
using Solen.Core.Application.Settings.Organization.Queries;
using Solen.Core.Application.SigningUp.EventsHandlers.Organizations;
using Solen.Core.Application.SigningUp.Organizations.Commands;
using Solen.Core.Application.UserProfile.Queries;
using Solen.Core.Application.Users.Commands;
using Solen.Core.Application.Users.EventsHandlers.Commands;
using Solen.Infrastructure.MediatR;

namespace Solen.DependencyInjection.Infrastructure
{
    public static class MediatR
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            var assemblies = new Collection<Assembly>
            {
                typeof(LoginUserQueryHandler).GetTypeInfo().Assembly,
                typeof(GetCourseQueryHandler).GetTypeInfo().Assembly,
                typeof(SendNotificationsToCourseLearners).GetTypeInfo().Assembly,
                typeof(CreateCourseCommandHandler).GetTypeInfo().Assembly,
                typeof(AddLearnerAccessHistoryCommand).GetTypeInfo().Assembly,
                typeof(GetNotificationsQueryHandler).GetTypeInfo().Assembly,
                typeof(CreateLearningPathCommandHandler).GetTypeInfo().Assembly,
                typeof(InitSigningUpCommandHandler).GetTypeInfo().Assembly,
                typeof(SendNotificationToCompleteSigningUp).GetTypeInfo().Assembly,
                typeof(InviteMembersCommandHandler).GetTypeInfo().Assembly,
                typeof(SendNotificationToUsersToCompleteSigningUp).GetTypeInfo().Assembly,
                typeof(GetNotificationTemplatesQueryHandler).GetTypeInfo().Assembly,
                typeof(GetOrganizationInfoQueryHandler).GetTypeInfo().Assembly,
                typeof(SendNotificationToResetPassword).GetTypeInfo().Assembly,
                typeof(GetCoursesInfoQueryHandler).GetTypeInfo().Assembly,
                typeof(GetCurrentUserInfoQueryHandler).GetTypeInfo().Assembly,
            };

            mvcBuilder.AddFluentValidation(c =>
            {
                c.RegisterValidatorsFromAssemblies(assemblies);
                c.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory);
            });

            services.AddMediatR(assemblies.ToArray());

            services.AddTransient(typeof(IRequestPreProcessor<>), typeof(RequestLogger<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}
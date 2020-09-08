using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.Notifications.Commands;
using Solen.Core.Application.Notifications.Queries;
using Solen.Core.Application.Notifications.Services.Commands;
using Solen.Core.Application.Notifications.Services.Queries;
using Solen.Persistence.Notifications.Commands;
using Solen.Persistence.Notifications.Queries;
using Solen.WebApi.Notifications;

namespace Solen.DependencyInjection.Application
{
    public static class Notifications
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            // Controllers 
            mvcBuilder.AddApplicationPart(typeof(NotificationsController).GetTypeInfo().Assembly);

            //------------------------- Queries
            // GetNotifications
            services.AddScoped<IGetNotificationsService, GetNotificationsService>();
            services.AddScoped<IGetNotificationsRepository, GetNotificationsRepository>();
            
            //------------------------- Commands
            // MarkNotificationAsRead
            services.AddScoped<IMarkNotificationAsReadService, MarkNotificationAsReadService>();
            services.AddScoped<IMarkNotificationAsReadRepository, MarkNotificationAsReadRepository>();
            
            return services;
        }
    }
}

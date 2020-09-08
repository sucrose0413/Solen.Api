using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.Common.Notifications.Impl;
using Solen.Infrastructure.Notifications;
using Solen.Persistence.Common.Notifications;


namespace Solen.DependencyInjection.Common
{
    public static class Notifications
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            
            // messaging
            services.AddScoped<INotificationMessageGenerator, NotificationMessageGenerator>();
            services.AddScoped<INotificationsRepo, NotificationsRepo>();
            services.AddScoped<INotificationMessageHandler, NotificationMessageHandler>();
 

            return services;
        }
        
    }
}
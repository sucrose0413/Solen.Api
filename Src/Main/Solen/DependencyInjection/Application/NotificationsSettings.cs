using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.Settings.Notifications.Commands;
using Solen.Core.Application.Settings.Notifications.Queries;
using Solen.Core.Application.Settings.Notifications.Services.Commands;
using Solen.Core.Application.Settings.Notifications.Services.Queries;
using Solen.Persistence.Settings.Notifications.Commands;
using Solen.Persistence.Settings.Notifications.Queries;
using Solen.WebApi.Settings.Notifications;


namespace Solen.DependencyInjection.Application
{
    public static class NotificationsSettings
    {
        public static IServiceCollection AddNotificationsSettings(this IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            // Controllers 
            mvcBuilder.AddApplicationPart(typeof(NotificationTemplatesController).GetTypeInfo().Assembly);

            //------------------------- Queries
            // GetNotificationTemplates
            services.AddScoped<IGetNotificationTemplatesService, GetNotificationTemplatesService>();
            services.AddScoped<IGetNotificationTemplatesRepository, GetNotificationTemplatesRepository>();
            // GetNotificationTemplate
            services.AddScoped<IGetNotificationTemplateService, GetNotificationTemplateService>();
            services.AddScoped<IGetNotificationTemplateRepository, GetNotificationTemplateRepository>();


            //------------------------- Commands
            // UpdateNotificationTemplate
            services.AddScoped<IToggleNotificationActivationService, ToggleNotificationActivationService>();
            services.AddScoped<IToggleNotificationActivationRepository, ToggleNotificationActivationRepository>();
            
            return services;
        }
    }
}
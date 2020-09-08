using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.Common.Notifications.Impl;
using Solen.Core.Application.UnitOfWork;
using Solen.Infrastructure.Notifications;
using Solen.Infrastructure.Notifications.Emailing;
using Solen.Infrastructure.Notifications.Emailing.Pickup;
using Solen.Infrastructure.Notifications.Emailing.SendGrid;
using Solen.Infrastructure.Notifications.WebSocket;


namespace Solen.DependencyInjection.Infrastructure
{
    public static class Notifications
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services,
            IConfiguration configuration)
        {
            // emailing 
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            if (configuration.GetValue<bool>("EmailSettings:IsPickupDirectory"))
            {
                services.AddTransient<IEmailSender, EmlEmailSender>();
            }
            else
            {
                services.AddTransient<IEmailSender, SendGridEmailSender>();
            }

            services.AddScoped<INotificationMessageHandler, NotificationMessageHandler>();
            services.AddScoped<INotificationMessageDispatcher, NotificationMessageDispatcher>(
                NotificationDispatchersFactory()
            );

            return services;
        }

        private static Func<IServiceProvider, NotificationMessageDispatcher> NotificationDispatchersFactory()
        {
            return serviceProvider =>
            {
                var emailSender = serviceProvider.GetService<IEmailSender>();
                var hubContext = serviceProvider.GetService<IHubContext<ClientHub>>();
                var repo = serviceProvider.GetService<INotificationsRepo>();
                var uniteOfWork = serviceProvider.GetService<IUnitOfWork>();

                var notificationSenders = new List<INotificationSender>
                {
                    new EmailNotificationSender(emailSender),
                    new WebSocketNotificationSender(hubContext, repo, uniteOfWork)
                };

                return new NotificationMessageDispatcher(notificationSenders);
            };
        }
    }
}
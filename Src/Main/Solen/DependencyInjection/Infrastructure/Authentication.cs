using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Solen.Core.Domain.Identity.Enums;
using Solen.WebApi;

namespace Solen.DependencyInjection.Infrastructure
{
    public static class Authentication
    {
        public static IServiceCollection AddAppAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration.GetSection("Security:Key").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/ws")))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(AuthorizationPolicies.AdminPolicy,
                    configurePolicy => configurePolicy.RequireRole(UserRoles.Admin));

                config.AddPolicy(AuthorizationPolicies.CoursesManagementPolicy,
                    configurePolicy => configurePolicy.RequireRole(UserRoles.Admin, UserRoles.Instructor));

                config.AddPolicy(AuthorizationPolicies.UsersManagementPolicy,
                    configurePolicy => configurePolicy.RequireRole(UserRoles.Admin));

                config.AddPolicy(AuthorizationPolicies.SettingsPolicy,
                    configurePolicy => configurePolicy.RequireRole(UserRoles.Admin));

                config.AddPolicy(AuthorizationPolicies.InstructorPolicy,
                    configurePolicy => configurePolicy.RequireRole(UserRoles.Admin, UserRoles.Instructor));

                config.AddPolicy(AuthorizationPolicies.LearnerPolicy,
                    configurePolicy =>
                        configurePolicy.RequireRole(UserRoles.Admin, UserRoles.Instructor, UserRoles.Learner));
            });

            return services;
        }
    }
}
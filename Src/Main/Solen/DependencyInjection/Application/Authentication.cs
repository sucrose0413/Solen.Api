using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.Auth.Commands;
using Solen.Core.Application.Auth.EventsHandlers.PasswordTokenSet;
using Solen.Core.Application.Auth.Queries;
using Solen.Core.Application.Auth.Services.Commands;
using Solen.Core.Application.Auth.Services.Queries;
using Solen.Persistence.Auth.Queries;
using Solen.WebApi.Auth.Controllers;

namespace Solen.DependencyInjection.Application
{
    public static class Authentication
    {
        public static IServiceCollection AddApplicationAuth(this IServiceCollection services,
            IMvcBuilder mvcBuilder, IConfiguration configuration)
        {
            // Controllers 
            mvcBuilder.AddApplicationPart(typeof(AuthController).GetTypeInfo().Assembly);
            
            //------------------------- Commands
            // ForgotPassword
            services.AddScoped<IForgotPasswordService, ForgotPasswordService>();
            services.Configure<ResetPasswordPageInfo>(configuration.GetSection("ResetPasswordPageInfo"));
            // ResetPassword
            services.AddScoped<IResetPasswordService, ResetPasswordService>();
            
            //------------------------- Queries
            // Common
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            // LoginUser
            services.AddScoped<ILoginUserService, LoginUserService>();
            // GetCurrentLoggedUser
            services.AddScoped<IGetCurrentLoggedUserService, GetCurrentLoggedUserService>();
            // CheckPasswordToken
            services.AddScoped<ICheckPasswordTokenService, CheckPasswordTokenService>();
            // RefreshToken
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            
            return services;
        }
    }
}
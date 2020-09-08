using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.Users.Commands;
using Solen.Core.Application.Users.EventsHandlers.Commands;
using Solen.Core.Application.Users.Queries;
using Solen.Core.Application.Users.Services.Commands;
using Solen.Core.Application.Users.Services.Queries;
using Solen.Persistence.Users.Commands;
using Solen.Persistence.Users.Queries;
using Solen.WebApi.Users;

namespace Solen.DependencyInjection.Application
{
    public static class UsersManagement
    {
        public static IServiceCollection AddUsersManagement(this IServiceCollection services, IMvcBuilder mvcBuilder,
            IConfiguration configuration)
        {
            // Controllers 
            mvcBuilder.AddApplicationPart(typeof(UsersController).GetTypeInfo().Assembly);

            //------------------------- Commands
            // InviteMembers
            services.AddScoped<IInviteMembersService, InviteMembersService>();
            services.AddScoped<IInviteMembersRepository, InviteMembersRepository>();
            // UpdateUserLearningPath
            services.AddScoped<IUpdateUserLearningPathService, UpdateUserLearningPathService>();
            services.AddScoped<IUpdateUserLearningPathRepository, UpdateUserLearningPathRepository>();
            // UpdateUserRoles
            services.AddScoped<IUpdateUserRolesService, UpdateUserRolesService>();
            services.AddScoped<IUpdateUserRolesRepository, UpdateUserRolesRepository>();
            // BlockUser
            services.AddScoped<IBlockUserService, BlockUserService>();
            // UnblockUser
            services.AddScoped<IUnblockUserService, UnblockUserService>();
            
            //------------------------- Queries
            // GetUsersList
            services.AddScoped<IGetUsersListService, GetUsersListService>();
            // GetUser
            services.AddScoped<IGetUserService, GetUserService>();
            services.AddScoped<IGetUserRepository, GetUserRepository>();
            
            
            // Events Handlers
            services.Configure<CompleteUserSigningUpPageInfo>(
                configuration.GetSection("CompleteUserSigningUpPageInfo"));


            return services;
        }
    }
}
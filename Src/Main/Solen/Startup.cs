using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Serilog;
using Solen.DependencyInjection.Application;
using Solen.DependencyInjection.Common;
using Solen.DependencyInjection.Infrastructure;
using Solen.Infrastructure.Notifications.WebSocket;
using Solen.Middlewares;

namespace Solen
{
    public class Startup
    {
        private readonly string _allowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // this method will be called in a production environment
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // MVC
            var mvcBuilder = services.AddControllers()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .AddControllersAsServices();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            services.Configure<FormOptions>(x => { x.MultipartBodyLengthLimit = 2147483648; });


            var corsOrigins = Configuration.GetValue<string>("AppSettings:CorsOrigins");
            if (corsOrigins != null)
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(_allowSpecificOrigins,
                        builder =>
                        {
                            builder.WithOrigins(corsOrigins.Split(';'))
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                        });
                });
            }


            services.AddSignalR();

            services.AddInfrastructure(mvcBuilder, Configuration)
                .AddApplicationCore(mvcBuilder, Configuration)
                .AddApplicationCommon();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseErrorHandlingMiddleware();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseCors(_allowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();
     
            app.UseStaticFiles();

            if (!env.IsEnvironment("IntegrationTests"))
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider =
                        new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                    RequestPath = new PathString("/Resources")
                });
            }

            // Swagger
            if (Configuration.GetValue<bool>("SwaggerOptions:Enable"))
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(
                        Configuration.GetValue<string>("SwaggerOptions:Url"),
                        Configuration.GetValue<string>("SwaggerOptions:Name"));
                });
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
                endpoints.MapDefaultControllerRoute().RequireAuthorization();
                endpoints.MapHub<ClientHub>("/ws/events");
            });
        }
    }
}
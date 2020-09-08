using System.IO;
using System.Linq;
using System.Net.Http;
using IdentityModel.Client;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Persistence;

namespace Solen.SpecTests
{
    public abstract class BaseWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        private readonly string _connectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;
        protected IUserManager UserManager;
        protected SolenDbContext Context;
        private IServiceScope _scope;

        protected BaseWebApplicationFactory()
        {
            _connection = new SqliteConnection(_connectionString);
            _connection.Open();
        }

        protected override IWebHostBuilder CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder().UseStartup<TStartup>();

        protected override void ConfigureWebHost(IWebHostBuilder builder) =>
            builder.ConfigureServices(services =>
                {
                    var descriptor =
                        services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SolenDbContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services
                        .AddEntityFrameworkSqlite()
                        .AddDbContext<SolenDbContext>(options =>
                        {
                            options.UseSqlite(_connection);
                            options.UseInternalServiceProvider(services.BuildServiceProvider());
                        });

                    var sp = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database and UserManager
                    _scope = sp.CreateScope();
                    var scopedServices = _scope.ServiceProvider;

                    Context = scopedServices.GetRequiredService<SolenDbContext>();
                    // Ensure the database is created.
                    Context.Database.EnsureCreated();
                    UserManager = scopedServices.GetRequiredService<IUserManager>();
                }).ConfigureAppConfiguration(AppConfig)
                .UseEnvironment("IntegrationTests");

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connection.Close();
            _scope.Dispose();
        }

        protected void AppConfig(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.tests.json");
            builder.AddJsonFile(configPath);
        }

        public HttpClient GetAuthenticatedClient(User user)
        {
            var client = CreateClient();

            var claims = UserManager.CreateUserClaims(user);
            var token = UserManager.CreateUserToken(claims);
            client.SetBearerToken(token);

            return client;
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }
        
        public void CreateOrganization(Organization organization)
        {
            Context.Organizations.Add(organization);
            Context.SaveChanges();
        }
        
        public void CreateUser(User user, string password = null)
        {
            UserManager.CreateAsync(user).Wait();
            
            if (password != null)
                UserManager.UpdatePassword(user, password);
            
            Context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            UserManager.UpdateUser(user);
            Context.SaveChanges();
        }

        public void AddLearningPath(LearningPath learningPath)
        {
            Context.LearningPaths.Add(learningPath);
            Context.SaveChanges();
        }
    }
}
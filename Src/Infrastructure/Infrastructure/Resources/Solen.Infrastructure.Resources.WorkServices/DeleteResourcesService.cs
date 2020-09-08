using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Solen.Core.Application.Common.Resources.Impl;
using Solen.Core.Application.UnitOfWork;


namespace Solen.Infrastructure.Resources.WorkServices
{
    public class DeleteResourcesService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;


        public DeleteResourcesService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var serviceProvider = scope.ServiceProvider;

                    var resourceFactory = serviceProvider.GetRequiredService<IResourceStorageManagerFactory>();
                    var repo = serviceProvider.GetRequiredService<IAppResourceManagerRepo>();
                    var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

                    var resourcesToDelete = await repo.GetResourcesToDelete(stoppingToken);
                    foreach (var resource in resourcesToDelete)
                    {
                        var resourceAccessor = resourceFactory.Create(resource.ResourceType);
                        if (resourceAccessor.Delete(resource.Id))
                            repo.RemoveResource(resource);
                    }

                    await unitOfWork.SaveAsync(stoppingToken);
                }
                catch (Exception)
                {
                    // ignored
                }


                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            }
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Solen.Core.Application.Common.Security;

namespace Solen.Infrastructure.MediatR
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public RequestLogger(ILogger<TRequest> logger, ICurrentUserAccessor currentUserAccessor)
        {
            _logger = logger;
            _currentUserAccessor = currentUserAccessor;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            _logger.LogInformation("Solen Request: {Name} {@UserEmail}", 
                name, _currentUserAccessor.UserEmail);

            return Task.CompletedTask;
        }
    }
}
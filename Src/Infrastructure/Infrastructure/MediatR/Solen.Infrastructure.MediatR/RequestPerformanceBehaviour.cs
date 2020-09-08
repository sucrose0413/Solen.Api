using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Solen.Core.Application.Common.Security;

namespace Solen.Infrastructure.MediatR
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger, ICurrentUserAccessor currentUserAccessor)
        {
            _timer = new Stopwatch();

            _logger = logger;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                var name = typeof(TRequest).Name;

                _logger.LogWarning(
                    "Solen Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserEmail}",
                    name, _timer.ElapsedMilliseconds, _currentUserAccessor.UserEmail);
            }

            return response;
        }
    }
}
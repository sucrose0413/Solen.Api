using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.SigningUp.Users.Queries
{
    public class CheckUserSigningUpTokenQueryHandler : IRequestHandler<CheckUserSigningUpTokenQuery, Unit>
    {
        private readonly ICheckUserSigningUpTokenService _service;

        public CheckUserSigningUpTokenQueryHandler(ICheckUserSigningUpTokenService service)
        {
            _service = service;
        }

        public async Task<Unit> Handle(CheckUserSigningUpTokenQuery query, CancellationToken token)
        {
            await _service.CheckSigningUpToken(query.SigningUpToken, token);

            return Unit.Value;
        }
    }
}
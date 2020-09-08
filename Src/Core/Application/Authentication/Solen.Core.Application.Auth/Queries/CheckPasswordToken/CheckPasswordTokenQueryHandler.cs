using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Auth.Queries
{
    public class CheckPasswordTokenQueryHandler : IRequestHandler<CheckPasswordTokenQuery, Unit>
    {
        private readonly ICheckPasswordTokenService _service;

        public CheckPasswordTokenQueryHandler(ICheckPasswordTokenService service)
        {
            _service = service;
        }

        public async Task<Unit> Handle(CheckPasswordTokenQuery query, CancellationToken token)
        {
            await _service.CheckPasswordToken(query.PasswordToken, token);

            return Unit.Value;
        }
    }
}
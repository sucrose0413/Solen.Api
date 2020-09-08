using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.SigningUp.Organizations.Queries
{
    public class
        CheckOrganizationSigningUpTokenQueryHandler : IRequestHandler<CheckOrganizationSigningUpTokenQuery, Unit>
    {
        private readonly ICheckOrganizationSigningUpTokenService _service;

        public CheckOrganizationSigningUpTokenQueryHandler(ICheckOrganizationSigningUpTokenService service)
        {
            _service = service;
        }

        public async Task<Unit> Handle(CheckOrganizationSigningUpTokenQuery query, CancellationToken token)
        {
            await _service.CheckSigningUpToken(query.SigningUpToken, token);

            return Unit.Value;
        }
    }
}
using MediatR;

namespace Solen.Core.Application.SigningUp.Organizations.Queries
{
    public class CheckOrganizationSigningUpTokenQuery : IRequest
    {
        public string SigningUpToken { get; set; }
    }
}
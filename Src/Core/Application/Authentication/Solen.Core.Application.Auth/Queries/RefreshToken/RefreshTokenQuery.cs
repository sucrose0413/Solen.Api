using MediatR;

namespace Solen.Core.Application.Auth.Queries
{
    public class RefreshTokenQuery : IRequest<LoggedUserViewModel>
    {
        public RefreshTokenQuery(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public string RefreshToken { get; }
    }
}
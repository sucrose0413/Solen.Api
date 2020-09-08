using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Auth.Queries
{
    public class GetCurrentLoggedUserQueryHandler : IRequestHandler<GetCurrentLoggedUserQuery, LoggedUserDto>
    {
        private readonly IGetCurrentLoggedUserService _service;
        private readonly ICommonService _commonService;

        public GetCurrentLoggedUserQueryHandler(IGetCurrentLoggedUserService service, ICommonService commonService)
        {
            _service = service;
            _commonService = commonService;
        }

        public async Task<LoggedUserDto> Handle(GetCurrentLoggedUserQuery query, CancellationToken token)
        {
            var currentUser = await _service.GetCurrentUser(token);

            _commonService.CheckIfUserIsBlockedOrInactive(currentUser);

            return _commonService.CreateLoggedUser(currentUser);
        }
    }
}
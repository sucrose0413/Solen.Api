using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.UserProfile.Queries
{
    public class GetCurrentUserInfoQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileViewModel>
    {
        private readonly IGetCurrentUserInfoService _service;

        public GetCurrentUserInfoQueryHandler(IGetCurrentUserInfoService service)
        {
            _service = service;
        }

        public async Task<UserProfileViewModel> Handle(GetUserProfileQuery query, CancellationToken token)
        {
            return new UserProfileViewModel
            {
                CurrentUser = await _service.GetCurrentUserInfo(token)
            };
        }
    }
}
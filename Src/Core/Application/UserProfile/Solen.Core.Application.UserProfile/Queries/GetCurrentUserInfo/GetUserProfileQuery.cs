using MediatR;

namespace Solen.Core.Application.UserProfile.Queries
{
    public class GetUserProfileQuery : IRequest<UserProfileViewModel>
    {
    }
}
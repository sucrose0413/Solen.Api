using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Users.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel>
    {
        private readonly IGetUserService _service;

        public GetUserQueryHandler(IGetUserService service)
        {
            _service = service;
        }
        
        public async Task<UserViewModel> Handle(GetUserQuery query, CancellationToken token)
        {
            return new UserViewModel
            {
                User = await _service.GetUser(query.UserId, token),
                LearningPaths = await _service.GetLearningPaths(token),
                Roles = await _service.GetRoles(token)
            };
        }
    }
}
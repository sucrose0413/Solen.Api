using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Users.Queries
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, UsersListViewModel>
    {
        private readonly IGetUsersListService _service;

        public GetUsersListQueryHandler(IGetUsersListService service)
        {
            _service = service;
        }

        public async Task<UsersListViewModel> Handle(GetUsersListQuery query, CancellationToken token)
        {
            return new UsersListViewModel
            {
                Users = await _service.GetUsersList(token)
            };
        }
    }
}
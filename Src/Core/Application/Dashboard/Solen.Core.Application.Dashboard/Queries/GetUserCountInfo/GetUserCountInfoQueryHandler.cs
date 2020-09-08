using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Dashboard.Queries
{
    public class GetUserCountInfoQueryHandler : IRequestHandler<GetUserCountInfoQuery, UserCountInfoViewModel>
    {
        private readonly IGetUserCountInfoService _service;

        public GetUserCountInfoQueryHandler(IGetUserCountInfoService service)
        {
            _service = service;
        }

        public async Task<UserCountInfoViewModel> Handle(GetUserCountInfoQuery query, CancellationToken token)
        {
            return new UserCountInfoViewModel
            {
                UserCountInfo = await _service.GetUserCountInfo(token)
            };
        }
    }
}
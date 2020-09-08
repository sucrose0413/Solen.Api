using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Solen.Core.Application.Dashboard.Queries
{
    public class GetStorageInfoQueryHandler : IRequestHandler<GetStorageInfoQuery, StorageInfoViewModel>
    {
        private readonly IGetStorageInfoService _service;

        public GetStorageInfoQueryHandler(IGetStorageInfoService service)
        {
            _service = service;
        }

        public async Task<StorageInfoViewModel> Handle(GetStorageInfoQuery query, CancellationToken token)
        {
            return new StorageInfoViewModel
            {
                StorageInfo = await _service.GetStorageInfo(token)
            };
        }
    }
}
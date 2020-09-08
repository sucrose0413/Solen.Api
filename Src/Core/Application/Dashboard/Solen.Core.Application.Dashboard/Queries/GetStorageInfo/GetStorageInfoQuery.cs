using MediatR;

namespace Solen.Core.Application.Dashboard.Queries
{
    public class GetStorageInfoQuery : IRequest<StorageInfoViewModel>
    {
    }
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Learning.Queries;

namespace Solen.Core.Application.Learning.Services.Queries
{
    public class GetCompletedLecturesService : IGetCompletedLecturesService
    {
        private readonly IGetCompletedLecturesRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetCompletedLecturesService(IGetCompletedLecturesRepository repo,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }


        public async Task<IList<string>> GetLearnerCompletedLectures(string courseId, CancellationToken token)
        {
            return await _repo.GetLearnerCompletedLectures(courseId, _currentUserAccessor.UserId, token);
        }
    }
}
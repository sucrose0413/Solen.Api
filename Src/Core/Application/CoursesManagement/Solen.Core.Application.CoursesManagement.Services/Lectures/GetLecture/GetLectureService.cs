using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Lectures.Queries;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.Services.Lectures
{
    public class GetLectureService : IGetLectureService
    {
        private readonly IGetLectureRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetLectureService(IGetLectureRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }
        
        public async Task<LectureDto> GetLecture(string lectureId, CancellationToken token)
        {
            return await _repo.GetLecture(lectureId, _currentUserAccessor.OrganizationId, token) ??
                   throw new NotFoundException($"The lecture ({lectureId}) does not exist");
        }
    }
}
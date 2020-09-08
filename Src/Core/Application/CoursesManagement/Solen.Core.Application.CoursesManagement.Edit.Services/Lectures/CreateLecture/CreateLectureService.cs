using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Application.CoursesManagement.Edit.Services.Exceptions;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Lectures
{
    public class CreateLectureService : ICreateLectureService
    {
        private readonly ILectureCreatorFactory _factory;
        private readonly ICreateLectureRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public CreateLectureService(ILectureCreatorFactory factory, ICreateLectureRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _factory = factory;
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }
        
        public async Task ControlModuleExistenceAndCourseStatus(string moduleId, CancellationToken token)
        {
            var module = await _repo.GetModuleWithCourse(moduleId, _currentUserAccessor.OrganizationId, token) ??
                         throw new NotFoundException(nameof(Module), moduleId);

            if (!module.Course.IsEditable)
                throw new UnalterableCourseException();
        }

        public Lecture CreateLecture(CreateLectureCommand command)
        {
            return _factory.Create(command);
        }

        public void UpdateLectureDuration(Lecture lecture, int duration)
        {
            lecture.UpdateDuration(duration);
        }

        public async Task AddLectureToRepo(Lecture lecture, CancellationToken token)
        {
            await _repo.AddLecture(lecture, token);
        }
    }
}
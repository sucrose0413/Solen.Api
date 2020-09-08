using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Courses
{
    public class CreateCourseService : ICreateCourseService
    {
        private readonly ICreateCourseRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;
        private readonly IDateTime _dateTime;

        public CreateCourseService(ICreateCourseRepository repo, ICurrentUserAccessor currentUserAccessor,
            IDateTime dateTime)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
            _dateTime = dateTime;
        }

        public Course CreateCourse(string title)
        {
            return new Course(title, _currentUserAccessor.UserId, _dateTime.Now);
        }

        public async Task AddCourseToRepo(Course course, CancellationToken token)
        {
            await _repo.AddCourse(course, token);
        }
    }
}
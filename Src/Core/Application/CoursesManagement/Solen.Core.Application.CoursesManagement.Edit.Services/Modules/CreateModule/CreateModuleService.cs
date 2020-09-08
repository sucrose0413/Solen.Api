using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;
using Solen.Core.Application.CoursesManagement.Edit.Services.Exceptions;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Modules
{
    public class CreateModuleService : ICreateModuleService
    {
        private readonly ICreateModuleRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public CreateModuleService(ICreateModuleRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task ControlCourseExistenceAndStatus(string courseId, CancellationToken token)
        {
            var course = await _repo.GetCourse(courseId, _currentUserAccessor.OrganizationId, token) ??
                         throw new NotFoundException(nameof(Course), courseId);

            if (!course.IsEditable)
                throw new UnalterableCourseException();
        }

        public Module CreateModule(CreateModuleCommand command)
        {
            return new Module(command.Name, command.CourseId, command.Order);
        }

        public async Task AddModuleToRepo(Module module, CancellationToken token)
        {
            await _repo.AddModule(module, token);
        }
    }
}
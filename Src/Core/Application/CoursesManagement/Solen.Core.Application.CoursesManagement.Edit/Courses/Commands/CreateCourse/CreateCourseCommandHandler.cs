using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CommandResponse>
    {
        private readonly ICreateCourseService _coursesService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCourseCommandHandler(ICreateCourseService coursesService, IUnitOfWork unitOfWork)
        {
            _coursesService = coursesService;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse> Handle(CreateCourseCommand command, CancellationToken token)
        {
            var courseToCreate = _coursesService.CreateCourse(command.Title);

            await _coursesService.AddCourseToRepo(courseToCreate, token);

            await _unitOfWork.SaveAsync(token);

            return new CommandResponse {Value = courseToCreate.Id};
        }
    }
}
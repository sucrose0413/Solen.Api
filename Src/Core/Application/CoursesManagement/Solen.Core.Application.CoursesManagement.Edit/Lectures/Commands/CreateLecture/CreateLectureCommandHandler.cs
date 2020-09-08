using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public class CreateLectureCommandHandler : IRequestHandler<CreateLectureCommand, CommandResponse>
    {
        private readonly ICreateLectureService _service;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLectureCommandHandler(ICreateLectureService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse> Handle(CreateLectureCommand command, CancellationToken token)
        {
            await _service.ControlModuleExistenceAndCourseStatus(command.ModuleId, token);

            var lectureToCreate = _service.CreateLecture(command);

            _service.UpdateLectureDuration(lectureToCreate, command.Duration);

            await _service.AddLectureToRepo(lectureToCreate, token);

            await _unitOfWork.SaveAsync(token);

            return new CommandResponse {Value = lectureToCreate.Id};
        }
    }
}
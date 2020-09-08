using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public class DeleteLectureCommandHandler : IRequestHandler<DeleteLectureCommand, CommandResponse>
    {
        private readonly IDeleteLectureService _service;
        private readonly ILecturesCommonService _commonService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public DeleteLectureCommandHandler(IDeleteLectureService service, ILecturesCommonService commonService,
            IUnitOfWork unitOfWork, IMediator mediator)
        {
            _service = service;
            _commonService = commonService;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<CommandResponse> Handle(DeleteLectureCommand command, CancellationToken token)
        {
            var lectureToDelete = await _commonService.GetLectureFromRepo(command.LectureId, token);

            _commonService.CheckCourseStatusForModification(lectureToDelete);

            _service.RemoveLectureFromRepo(lectureToDelete);

            await _unitOfWork.SaveAsync(token);

            await _mediator.Publish(new LectureDeletedEvent(lectureToDelete.Id), token);

            return new CommandResponse {Value = lectureToDelete.Id};
        }
    }
}
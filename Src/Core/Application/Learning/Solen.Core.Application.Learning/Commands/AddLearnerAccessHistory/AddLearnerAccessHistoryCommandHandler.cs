using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.Learning.Commands
{
    public class AddLearnerAccessHistoryCommandHandler : IRequestHandler<AddLearnerAccessHistoryCommand, Unit>
    {
        private readonly IAddLearnerAccessHistoryService _service;
        private readonly IUnitOfWork _unitOfWork;

        public AddLearnerAccessHistoryCommandHandler(IAddLearnerAccessHistoryService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddLearnerAccessHistoryCommand command, CancellationToken token)
        {
            var courseId = await _service.GetLectureCourseId(command.LectureId, token);

            await _service.UpdateOrCreateLearnerCourseAccessTime(courseId, token);

            var learnerAccessHistory = _service.CreateAccessHistory(command.LectureId);

            _service.AddLearnerLectureAccessHistoryToRepo(learnerAccessHistory);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
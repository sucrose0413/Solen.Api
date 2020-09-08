using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.Learning.Commands
{
    public class CompleteLectureCommandHandler : IRequestHandler<CompleteLectureCommand, Unit>
    {
        private readonly ICompleteLectureService _service;
        private readonly IUnitOfWork _unitOfWork;

        public CompleteLectureCommandHandler(ICompleteLectureService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CompleteLectureCommand command, CancellationToken token)
        {
            await _service.CheckLectureExistence(command.LectureId, token);
            
            if (await _service.IsTheLectureAlreadyCompleted(command.LectureId, token))
                return Unit.Value;

            var lectureCompleted = _service.CreateCompletedLecture(command.LectureId);

            _service.AddLearnerCompletedLectureToRepo(lectureCompleted);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
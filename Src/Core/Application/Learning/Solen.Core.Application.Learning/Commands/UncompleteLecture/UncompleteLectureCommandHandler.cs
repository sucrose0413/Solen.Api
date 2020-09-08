using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.Learning.Commands
{
    public class UncompleteLectureCommandHandler : IRequestHandler<UncompleteLectureCommand, Unit>
    {
        private readonly IUncompleteLectureService _service;
        private readonly IUnitOfWork _unitOfWork;
        private LearnerCompletedLecture _completedLecture;

        public UncompleteLectureCommandHandler(IUncompleteLectureService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(UncompleteLectureCommand command, CancellationToken token)
        {
            _completedLecture = await _service.GetCompletedLecture(command.LectureId, token);
            
            if(IsTheLectureAlreadyUncompleted())
                return Unit.Value;
            
            _service.RemoveLearnerCompletedLectureFromRepo(_completedLecture);

            await _unitOfWork.SaveAsync(token);
            
            return Unit.Value;
        }

        #region Private Methods

        private bool IsTheLectureAlreadyUncompleted()
        {
            return _completedLecture == null;
        }
        
        #endregion
    
    }
}
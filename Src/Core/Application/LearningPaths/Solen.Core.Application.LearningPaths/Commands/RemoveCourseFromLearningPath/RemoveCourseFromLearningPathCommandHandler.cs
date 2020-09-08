using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class RemoveCourseFromLearningPathCommandHandler : IRequestHandler<RemoveCourseFromLearningPathCommand, Unit>
    {
        private readonly IRemoveCourseFromLearningPathService _service;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveCourseFromLearningPathCommandHandler(IRemoveCourseFromLearningPathService service,
            IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveCourseFromLearningPathCommand command, CancellationToken token)
        {
            var learningPathCourse =
                await _service.GetLearningPathCourseFromRepo(command.LearningPathId, command.CourseId, token);

            if (learningPathCourse == null)
                return Unit.Value;

            _service.RemoveLearningPathCourseFromRepo(learningPathCourse);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
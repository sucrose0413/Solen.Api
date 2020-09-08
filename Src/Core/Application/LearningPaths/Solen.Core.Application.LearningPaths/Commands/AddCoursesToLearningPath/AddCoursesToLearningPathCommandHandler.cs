using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class AddCoursesToLearningPathCommandHandler : IRequestHandler<AddCoursesToLearningPathCommand, Unit>
    {
        private readonly IAddCoursesToLearningPathService _service;
        private readonly IUnitOfWork _unitOfWork;

        public AddCoursesToLearningPathCommandHandler(IAddCoursesToLearningPathService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddCoursesToLearningPathCommand command, CancellationToken token)
        {
            var learningPathToUpdate = await _service.GetLearningPathFromRepo(command.LearningPathId, token);

            foreach (var courseId in command.CoursesIds)
            {
                await _service.DoesCourseExist(courseId, token);
                _service.AddCourseToLearningPath(learningPathToUpdate, courseId);
            }

            _service.UpdateLearningPathRepo(learningPathToUpdate);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
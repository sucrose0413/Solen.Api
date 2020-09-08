using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.CoursesManagement.Edit.LearningPaths.Commands
{
    public class UpdateCourseLearningPathsCommandHandler : IRequestHandler<UpdateCourseLearningPathsCommand>
    {
        private readonly IUpdateCourseLearningPathsService _service;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCourseLearningPathsCommandHandler(IUpdateCourseLearningPathsService service,
            IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }


        public async Task<Unit> Handle(UpdateCourseLearningPathsCommand command, CancellationToken token)
        {
            var courseToUpdate = await _service.GetCourseFromRepo(command.CourseId, token);

            _service.CheckCourseStatusForModification(courseToUpdate);

            _service.RemoveEventualLearningPaths(courseToUpdate, command.LearningPathsIds);

            var learningPathsIdsToAdd = _service.GetLearningPathsIdsToAdd(courseToUpdate, command.LearningPathsIds);

            foreach (var learningPathId in learningPathsIdsToAdd)
                await _service.AddLearningPathToCourse(courseToUpdate, learningPathId, token);

            _service.UpdateCourseRepo(courseToUpdate);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
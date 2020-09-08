using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class UpdateLearningPathCommandHandler : IRequestHandler<UpdateLearningPathCommand, Unit>
    {
        private readonly IUpdateLearningPathService _service;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLearningPathCommandHandler(IUpdateLearningPathService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateLearningPathCommand command, CancellationToken token)
        {
            var learningPath = await _service.GetLearningPath(command.LearningPathId, token);

            _service.UpdateName(learningPath, command.Name);
            
            _service.UpdateDescription(learningPath, command.Description);

            _service.UpdateLearningPathRepo(learningPath);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.Users.Commands
{
    public class UpdateUserLearningPathCommandHandler : IRequestHandler<UpdateUserLearningPathCommand>
    {
        private readonly IUpdateUserLearningPathService _service;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserLearningPathCommandHandler(IUpdateUserLearningPathService service,
            IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserLearningPathCommand learningPathCommand, CancellationToken token)
        {
            var user = await _service.GetUserFromRepo(learningPathCommand.UserId, token);

            var learningPath = await _service.GetLearningPath(learningPathCommand.LearningPathId, token);

            _service.UpdateUserLearningPath(user, learningPath);

            _service.UpdateUserRepo(user);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
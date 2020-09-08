using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class DeleteLearningPathCommandHandler : IRequestHandler<DeleteLearningPathCommand, CommandResponse>
    {
        private readonly IDeleteLearningPathService _service;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLearningPathCommandHandler(IDeleteLearningPathService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse> Handle(DeleteLearningPathCommand command, CancellationToken token)
        {
            var learningPath = await _service.GetLearningPath(command.LearningPathId, token);

            _service.CheckIfDeletable(learningPath);

            var users = await _service.GetLearningPathUsers(learningPath.Id, token);

            if (users != null && users.Any())
            {
                var generalLearningPath = await _service.GetGeneralLearningPath(token);
                _service.ChangeUsersLearningPathToGeneral(users, generalLearningPath);
                _service.UpdateUsersRepo(users);
            }
            
            _service.RemoveLearningPathFromRepo(learningPath);

            await _unitOfWork.SaveAsync(token);

            return new CommandResponse {Value = learningPath.Id};
        }
    }
}
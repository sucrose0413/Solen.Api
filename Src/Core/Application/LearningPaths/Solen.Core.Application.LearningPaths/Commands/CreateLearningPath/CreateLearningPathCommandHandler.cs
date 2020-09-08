using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;


namespace Solen.Core.Application.LearningPaths.Commands
{
    public class CreateLearningPathCommandHandler : IRequestHandler<CreateLearningPathCommand, CommandResponse>
    {
        private readonly ICreateLearningPathService _service;
        private readonly IUnitOfWork _unitOfWork;


        public CreateLearningPathCommandHandler(ICreateLearningPathService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse> Handle(CreateLearningPathCommand command, CancellationToken token)
        {
            var learningPathToCreate = _service.CreateLearningPath(command.Name);

            await _service.AddLearningPathToRepo(learningPathToCreate, token);

            await _unitOfWork.SaveAsync(token);

            return new CommandResponse {Value = learningPathToCreate.Id};
        }
    }
}
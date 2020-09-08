using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.UserProfile.Commands
{
    public class UpdateCurrentUserPasswordCommandHandler : IRequestHandler<UpdateCurrentUserPasswordCommand>
    {
        private readonly IUpdateCurrentUserPasswordService _service;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCurrentUserPasswordCommandHandler(IUpdateCurrentUserPasswordService service,
            IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCurrentUserPasswordCommand command, CancellationToken token)
        {
            var currentUser = await _service.GetCurrentUser(token);

            _service.UpdateCurrentUserPassword(currentUser, command.NewPassword);

            _service.UpdateCurrentUserRepo(currentUser);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
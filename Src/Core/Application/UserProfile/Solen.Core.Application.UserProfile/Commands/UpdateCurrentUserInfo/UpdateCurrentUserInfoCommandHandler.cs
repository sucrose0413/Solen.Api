using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.UserProfile.Commands
{
    public class UpdateCurrentUserInfoCommandHandler : IRequestHandler<UpdateCurrentUserInfoCommand>
    {
        private readonly IUpdateCurrentUserInfoService _service;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCurrentUserInfoCommandHandler(IUpdateCurrentUserInfoService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCurrentUserInfoCommand command, CancellationToken token)
        {
            var currentUser = await _service.GetCurrentUser(token);

            _service.UpdateCurrentUserName(currentUser, command.UserName);

            _service.UpdateCurrentUserRepo(currentUser);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
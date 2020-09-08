using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.Users.Commands
{
    public class UnblockUserCommandHandler : IRequestHandler<UnblockUserCommand>
    {
        private readonly IUnblockUserService _service;
        private readonly IUnitOfWork _unitOfWork;

        public UnblockUserCommandHandler(IUnblockUserService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UnblockUserCommand command, CancellationToken token)
        {
            var user = await _service.GetUser(command.UserId, token);

            _service.UnblockUser(user);

            _service.UpdateUser(user);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
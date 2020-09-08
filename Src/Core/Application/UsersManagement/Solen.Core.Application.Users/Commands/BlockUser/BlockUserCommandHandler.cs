using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.Users.Commands
{
    public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand>
    {
        private readonly IBlockUserService _service;
        private readonly IUnitOfWork _unitOfWork;

        public BlockUserCommandHandler(IBlockUserService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(BlockUserCommand command, CancellationToken token)
        {
            _service.CheckIfTheUserToBlockIsTheCurrentUser(command.UserId);

            var user = await _service.GetUser(command.UserId, token);
            
            _service.BlockUser(user);
            
            _service.UpdateUser(user);

            await _unitOfWork.SaveAsync(token);
       
            return Unit.Value;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.Users.Commands
{
    public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommand>
    {
        private readonly IUpdateUserRolesService _service;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserRolesCommandHandler(IUpdateUserRolesService service,
            IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserRolesCommand command, CancellationToken token)
        {
            _service.CheckIfTheUserIsTheCurrentUser(command.UserId);

            var user = await _service.GetUserFromRepo(command.UserId, token);

            foreach (var roleId in command.RolesIds)
                await _service.CheckRole(roleId, token);

            _service.RemoveUserRoles(user);

            if (_service.DoesAdminRoleIncluded(command.RolesIds))
                _service.AddOnlyAdminRoleToUser(user);
            else
                _service.AddRolesToUser(user, command.RolesIds);

            _service.UpdateUserRepo(user);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.Users.Commands
{
    public class InviteMembersCommandHandler : IRequestHandler<InviteMembersCommand>
    {
        private readonly IInviteMembersService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public InviteMembersCommandHandler(IInviteMembersService service, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(InviteMembersCommand command, CancellationToken token)
        {
            var learningPath = await _service.GetLearningPathFromRepo(command.LearningPathId, token);

            foreach (var email in command.Emails)
                await _service.CheckEmailExistence(email);

            var users = _service.CreateUsers(command.Emails);
            foreach (var user in users)
            {
                _service.SetTheInviteeToUser(user);
                _service.SetTheInvitationTokenToUser(user);
                _service.UpdateUserLearningPath(user, learningPath);
                _service.AddLearnerRoleToUser(user);
                await _service.AddUserToRepo(user);
            }

            await _unitOfWork.SaveAsync(token);

            await _mediator.Publish(new MembersInvitedEvent(users), token);

            return Unit.Value;
        }
    }
}
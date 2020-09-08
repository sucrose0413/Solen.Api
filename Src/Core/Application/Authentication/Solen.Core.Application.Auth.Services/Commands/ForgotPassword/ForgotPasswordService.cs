using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.Auth.Commands;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Auth.Services.Commands
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly IUserManager _userManager;
        private readonly IRandomTokenGenerator _tokenGenerator;
        private readonly IMediator _mediator;

        public ForgotPasswordService(IUserManager userManager, IRandomTokenGenerator tokenGenerator, IMediator mediator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _mediator = mediator;
        }

        public async Task<User> GetUserFromRepo(string email, CancellationToken token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new NotFoundException("Unknown Email address");

            if (!_userManager.IsActiveUser(user))
                throw new LockedException("The user account is deactivated");

            return user;
        }

        public void SetUserPasswordToken(User user)
        {
            user.SetPasswordToken(_tokenGenerator.CreateToken());
        }

        public void UpdateUserRepo(User user)
        {
            _userManager.UpdateUser(user);
        }

        public async Task PublishPasswordTokenSetEvent(User user, CancellationToken token)
        {
            await _mediator.Publish(new PasswordTokenSetEvent(user), token);
        }
    }
}
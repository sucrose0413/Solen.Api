using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.SigningUp.Users.Commands
{
    public interface ICompleteUserSigningUpService
    {
        Task<User> GetUserByInvitationToken(string invitationToken);
        void UpdateUserName(User user, string userName);
        void ValidateUserInscription(User user, string password);
        void UpdateUserRepo(User user);
    }
}
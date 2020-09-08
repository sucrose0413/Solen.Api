using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.Auth.Queries
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoggedUserViewModel>
    {
        private readonly ILoginUserService _service;
        private readonly ICommonService _commonService;
        private readonly IUnitOfWork _unitOfWork;

        public LoginUserQueryHandler(ILoginUserService service, ICommonService commonService, IUnitOfWork unitOfWork)
        {
            _service = service;
            _commonService = commonService;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoggedUserViewModel> Handle(LoginUserQuery query, CancellationToken token)
        {
            var user = await _service.GetUserByEmail(query.Email, token);

            _service.CheckIfPasswordIsInvalid(user, query.Password);

            _commonService.CheckIfUserIsBlockedOrInactive(user);

            await _commonService.RemoveAnyUserRefreshToken(user, token);

            var newRefreshToken = _commonService.CreateNewRefreshToken(user);

            _commonService.AddNewRefreshTokenToRepo(newRefreshToken);

            await _unitOfWork.SaveAsync(token);

            return new LoggedUserViewModel
            {
                LoggedUser = _commonService.CreateLoggedUser(user),
                Token = _commonService.CreateUserToken(user),
                RefreshToken = newRefreshToken?.Id
            };
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.Auth.Queries
{
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, LoggedUserViewModel>
    {
        private readonly IRefreshTokenService _service;
        private readonly ICommonService _commonService;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenQueryHandler(IRefreshTokenService service, ICommonService commonService,
            IUnitOfWork unitOfWork)
        {
            _service = service;
            _commonService = commonService;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoggedUserViewModel> Handle(RefreshTokenQuery query, CancellationToken token)
        {
            var currentRefreshToken = await _service.GetCurrentRefreshToken(query.RefreshToken, token);

            _service.CheckRefreshTokenValidity(currentRefreshToken);

            var user = await _service.GetUserByRefreshToken(query.RefreshToken, token);

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
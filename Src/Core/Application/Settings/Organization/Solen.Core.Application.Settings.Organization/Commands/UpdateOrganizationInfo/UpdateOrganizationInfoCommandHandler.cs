using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.Settings.Organization.Commands
{
    public class UpdateOrganizationInfoCommandHandler : IRequestHandler<UpdateOrganizationInfoCommand>
    {
        private readonly IUpdateOrganizationInfoService _service;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrganizationInfoCommandHandler(IUpdateOrganizationInfoService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateOrganizationInfoCommand command, CancellationToken token)
        {
            var organizationToUpdate = await _service.GetOrganization(token);

            _service.UpdateOrganizationName(organizationToUpdate, command.OrganizationName);

            _service.UpdateOrganizationRepo(organizationToUpdate);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
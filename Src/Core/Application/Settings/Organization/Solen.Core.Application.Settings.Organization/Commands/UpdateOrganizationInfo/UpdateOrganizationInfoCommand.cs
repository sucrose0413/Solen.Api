using MediatR;

namespace Solen.Core.Application.Settings.Organization.Commands
{
    public class UpdateOrganizationInfoCommand : IRequest
    {
        public string OrganizationName { get; set; }
    }
}
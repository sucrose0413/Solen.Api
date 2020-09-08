using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Solen.Core.Application.Common.Security;

namespace Solen.Infrastructure.Security
{
    public class CurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext.User?.Claims
            ?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        public string Username => _httpContextAccessor.HttpContext.User?.Claims
            ?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

        public string UserEmail => _httpContextAccessor.HttpContext.User?.Claims
            ?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        public string OrganizationId => _httpContextAccessor.HttpContext.User?.Claims
            ?.FirstOrDefault(x => x.Type == "organizationId")?.Value;

        public string LearningPathId => _httpContextAccessor.HttpContext.User?.Claims
            ?.FirstOrDefault(x => x.Type == "learningPathId")?.Value;
    }
}
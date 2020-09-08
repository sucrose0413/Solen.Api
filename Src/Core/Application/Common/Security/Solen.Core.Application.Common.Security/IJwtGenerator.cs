using System.Collections.Generic;
using System.Security.Claims;

namespace Solen.Core.Application.Common.Security
{
    public interface IJwtGenerator
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}

using System;
using Microsoft.Extensions.Configuration;
using Solen.Core.Application.Common.Security;

namespace Solen.Infrastructure.Security
{
    public class SecurityConfig : ISecurityConfig
    {
        private readonly IConfiguration _config;

        public SecurityConfig(IConfiguration config)
        {
            _config = config;
        }

        public string GetSecurityKey()
        {
            return _config.GetSection("Security:Key").Value;
        }

        public int GetJwtTokenExpiryTimeInMinutes()
        {
            int.TryParse(_config.GetSection("Security:JwtTokenExpiryTimeInMinutes").Value, out var expiryTime);

            return expiryTime;
        }

        public int GetRefreshTokenExpiryTimeInDays()
        {
            int.TryParse(_config.GetSection("Security:RefreshTokenExpiryTimeInDays").Value, out var expiryTime);

            return expiryTime;
        }

        public bool IsSigninUpEnabled => Convert.ToBoolean(_config.GetSection("Security:IsSigninUpEnabled").Value);
    }
}
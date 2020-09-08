using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Solen.Core.Application.Common.Security;

namespace Solen.Infrastructure.Security
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly ISecurityConfig _config;

        public JwtGenerator(ISecurityConfig config)
        {
            _config = config;
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSecurityKey()));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var jwtTokenExpiryTimeInMinutes = _config.GetJwtTokenExpiryTimeInMinutes();
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(jwtTokenExpiryTimeInMinutes),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
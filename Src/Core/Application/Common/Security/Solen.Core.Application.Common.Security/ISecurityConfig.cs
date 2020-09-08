namespace Solen.Core.Application.Common.Security
{
    public interface ISecurityConfig
    {
        string GetSecurityKey();
        int GetJwtTokenExpiryTimeInMinutes();
        int GetRefreshTokenExpiryTimeInDays();
        bool IsSigninUpEnabled { get; }
    }
}
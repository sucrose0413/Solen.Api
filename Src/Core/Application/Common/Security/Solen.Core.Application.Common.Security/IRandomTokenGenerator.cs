namespace Solen.Core.Application.Common.Security
{
    public interface IRandomTokenGenerator
    {
        string CreateToken(int length = 100);
    }
}
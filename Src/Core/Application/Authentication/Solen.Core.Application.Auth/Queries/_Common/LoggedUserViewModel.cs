namespace Solen.Core.Application.Auth.Queries
{
    public class LoggedUserViewModel
    {
        public LoggedUserDto LoggedUser { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
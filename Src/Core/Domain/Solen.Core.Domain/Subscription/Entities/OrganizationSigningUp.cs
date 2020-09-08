using System;

namespace Solen.Core.Domain.Subscription.Entities
{
    public class OrganizationSigningUp
    {
        private OrganizationSigningUp()
        {
        }

        public OrganizationSigningUp(string email, string token)
        {
            Id = SignUpNewId;
            Email = email;
            Token = token;
        }

        public string Id { get; private set; }
        public string Email { get; private set; }
        public string Token { get; private set; }

        #region Private Methods

        private static string SignUpNewId => new Random().Next(0, 999999999).ToString("D9");

        #endregion
    }
}
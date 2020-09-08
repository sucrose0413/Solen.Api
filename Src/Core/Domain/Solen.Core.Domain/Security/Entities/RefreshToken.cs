using System;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Domain.Security.Entities
{
    public class RefreshToken
    {
        private RefreshToken()
        {
        }

        public RefreshToken(User user, DateTime? expiryTime)
        {
            Id = NewTokenId;
            UserId = user.Id;
            ExpiryTime = expiryTime;
        }

        public string Id { get; private set; }
        public string UserId { get; private set; }
        public User User { get; private set; }
        public DateTime? ExpiryTime { get; private set; }

        #region Private Methods

        private static string NewTokenId => Guid.NewGuid().ToString("N");

        #endregion
    }
}
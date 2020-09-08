using System;
using System.Linq;
using System.Security.Cryptography;
using Solen.Core.Application.Common.Security;

namespace Solen.Infrastructure.Security
{
    public class RandomTokenGenerator : IRandomTokenGenerator
    {
        public string CreateToken(int length = 100)
        {
            const string availableChars =
                "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            using var generator = new RNGCryptoServiceProvider();
            var bytes = new byte[length];
            generator.GetBytes(bytes);
            var chars = bytes
                .Select(b => availableChars[b % availableChars.Length]);
            var token = new string(chars.ToArray());

            var unixTimestamp = (int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return token + unixTimestamp;
        }
    }
}
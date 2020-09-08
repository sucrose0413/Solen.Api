using System;

namespace Solen.Core.Application.Exceptions
{
    public class AppTechnicalException : Exception
    {
        public AppTechnicalException(string message) : base(message)
        {
        }
    }
}
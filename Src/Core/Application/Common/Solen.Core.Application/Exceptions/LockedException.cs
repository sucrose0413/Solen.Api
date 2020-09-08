using System;


namespace Solen.Core.Application.Exceptions
{
    public class LockedException : Exception
    {
        private LockedException()
        {
        }

        public LockedException(string message)
            : base(message)
        {
        }
    }
}
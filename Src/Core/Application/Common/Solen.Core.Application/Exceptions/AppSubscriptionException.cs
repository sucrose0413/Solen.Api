using System;
using System.Collections.Generic;

namespace Solen.Core.Application.Exceptions
{
    public class AppSubscriptionException : Exception
    {
        public AppSubscriptionException(string failure)
            : base(failure)
        {
            Failures.Add(failure);
        }

        public IList<string> Failures { get; } = new List<string>();
    }
}

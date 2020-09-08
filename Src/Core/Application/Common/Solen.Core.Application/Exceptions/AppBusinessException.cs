using System;
using System.Collections.Generic;

namespace Solen.Core.Application.Exceptions
{
    public class AppBusinessException : Exception
    {

        public AppBusinessException(IEnumerable<string> failures)
        {
            foreach (var failure in failures)
                Failures.Add(failure);
        }

        public AppBusinessException(string failure)
            : base(failure)
        {
            Failures.Add(failure);
        }

        public IList<string> Failures { get; } = new List<string>();
    }
}
using System;

namespace Solen.Core.Application.Common.Security
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}
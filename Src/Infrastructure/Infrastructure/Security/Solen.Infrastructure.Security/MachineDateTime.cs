using System;
using Solen.Core.Application.Common.Security;

namespace Solen.Infrastructure.Security
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
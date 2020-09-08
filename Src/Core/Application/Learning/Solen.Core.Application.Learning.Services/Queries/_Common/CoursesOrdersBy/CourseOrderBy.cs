using Solen.Core.Domain.Common;

namespace Solen.Core.Application.Learning.Services.Queries
{
    public abstract class CourseOrderBy : Enumeration
    {
        protected CourseOrderBy(int value, string name) : base(value, name)
        {
        }
    }
}
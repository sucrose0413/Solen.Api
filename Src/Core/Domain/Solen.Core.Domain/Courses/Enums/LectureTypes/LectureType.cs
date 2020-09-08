using Solen.Core.Domain.Common;

namespace Solen.Core.Domain.Courses.Enums.LectureTypes
{
    public abstract class LectureType : Enumeration
    {
        protected LectureType(int value, string name) : base(value, name)
        {
        }

        public abstract bool IsMediaLecture { get; }
    }
}
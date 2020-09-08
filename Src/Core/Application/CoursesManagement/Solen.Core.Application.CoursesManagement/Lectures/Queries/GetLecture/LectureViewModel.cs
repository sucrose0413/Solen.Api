using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.Lectures.Queries
{
    public class LectureViewModel
    {
        public LectureViewModel(LectureDto lecture)
        {
            Lecture = lecture;
        }

        public LectureDto Lecture { get; }
    }
}
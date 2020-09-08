using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Lectures
{
    public interface IUpdateLectureRepository
    {
        void UpdateLecture(Lecture lecture);
    }
}
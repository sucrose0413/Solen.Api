using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Lectures
{
    public interface IDeleteLectureRepository
    {
        void RemoveLecture(Lecture lecture);
    }
}
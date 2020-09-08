using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public interface IDeleteLectureService
    {
        void RemoveLectureFromRepo(Lecture lecture);
    }
}
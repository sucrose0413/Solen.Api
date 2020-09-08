using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.LectureTypes;

namespace Solen.Core.Application.CoursesManagement.Edit.Factories.LectureCreation
{
    public interface ILectureCreator
    {
        Lecture Create(CreateLectureCommand command);
        LectureType LectureType { get; }
    }
}
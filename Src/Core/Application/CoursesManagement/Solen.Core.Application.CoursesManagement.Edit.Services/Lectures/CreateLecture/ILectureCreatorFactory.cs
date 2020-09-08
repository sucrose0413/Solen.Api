using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Lectures
{
    public interface ILectureCreatorFactory
    {
        Lecture Create(CreateLectureCommand command);
    }
}
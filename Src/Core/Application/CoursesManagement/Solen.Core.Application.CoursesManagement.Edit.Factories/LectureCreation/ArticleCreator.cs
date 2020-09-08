using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.LectureTypes;
using ArticleLecture = Solen.Core.Domain.Courses.Enums.LectureTypes.ArticleLecture;

namespace Solen.Core.Application.CoursesManagement.Edit.Factories.LectureCreation
{
    public class ArticleCreator : ILectureCreator
    {
        public Lecture Create(CreateLectureCommand command)
        {
            return new Domain.Courses.Entities.ArticleLecture(command.Title, command.ModuleId, command.Order, command.Content);
        }

        public LectureType LectureType => new ArticleLecture();
    }
}
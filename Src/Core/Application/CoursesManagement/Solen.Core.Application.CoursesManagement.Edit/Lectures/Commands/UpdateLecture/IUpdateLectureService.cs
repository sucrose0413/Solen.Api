using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public interface IUpdateLectureService
    {
        void UpdateLectureTitle(Lecture lecture, string title);
        void UpdateLectureDuration(Lecture lecture, int duration);
        void UpdateArticleContent(ArticleLecture article, string content);
        void UpdateLectureRepo(Lecture lecture);
    }
}
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Lectures
{
    public class UpdateLectureService : IUpdateLectureService
    {
        private readonly IUpdateLectureRepository _repo;

        public UpdateLectureService(IUpdateLectureRepository repo)
        {
            _repo = repo;
        }

        public void UpdateLectureTitle(Lecture lecture, string title)
        {
            lecture.UpdateTitle(title);
        }

        public void UpdateLectureDuration(Lecture lecture, int duration)
        {
            lecture.UpdateDuration(duration);
        }

        public void UpdateArticleContent(ArticleLecture article, string content)
        {
            article.UpdateContent(content);
        }

        public void UpdateLectureRepo(Lecture lecture)
        {
            _repo.UpdateLecture(lecture);
        }
    }
}
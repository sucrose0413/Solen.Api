using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Lectures
{
    public class DeleteLectureService : IDeleteLectureService
    {
        private readonly IDeleteLectureRepository _repo;

        public DeleteLectureService(IDeleteLectureRepository repo)
        {
            _repo = repo;
        }
        
        public void RemoveLectureFromRepo(Lecture lecture)
        {
            _repo.RemoveLecture(lecture);
        }
    }
}
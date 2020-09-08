using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public interface ICreateLectureService
    { 
        Task ControlModuleExistenceAndCourseStatus(string moduleId, CancellationToken token);
        Lecture CreateLecture(CreateLectureCommand command);
        void UpdateLectureDuration(Lecture lecture, int duration);
        Task AddLectureToRepo(Lecture lecture, CancellationToken token);
    }
}
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public interface ILecturesCommonService
    {
        Task<Lecture> GetLectureFromRepo(string lectureId, CancellationToken token);
        void CheckCourseStatusForModification(Lecture lecture);
    }
}
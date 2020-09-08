using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Lectures
{
    public interface IUploadMediaRepository
    {
        void UpdateLecture(Lecture lecture);
        Task<LectureModuleIdCourseId> GetLectureModuleIdAndCourseId(string lectureId, CancellationToken token);
        Task AddCourseResource(CourseResource courseResource, CancellationToken token);
    }
}
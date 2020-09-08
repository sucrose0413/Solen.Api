using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Lectures
{
    public interface ICreateLectureRepository
    {
        Task AddLecture(Lecture lecture, CancellationToken token);
        Task<Module> GetModuleWithCourse(string moduleId, string organizationId, CancellationToken token);
    }
}
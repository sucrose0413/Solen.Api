using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public interface ICreateCourseService
    {
        Course CreateCourse(string title);
        Task AddCourseToRepo(Course course, CancellationToken token);
    }
}
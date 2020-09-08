using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Courses
{
    public interface IUpdateModulesOrdersRepository
    {
        Task<List<Module>> GetCourseModules(string courseId, CancellationToken token);
        void UpdateModules(IEnumerable<Module> modules);
    }
}
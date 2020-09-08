using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Resources.Entities;

namespace Solen.Core.Application.CoursesManagement.EventsHandlers.Courses
{
    public interface ICourseResourcesRepo
    {
        Task<IList<AppResource>> GetCourseResources(string courseId, CancellationToken token);
    }
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Resources.Entities;

namespace Solen.Core.Application.CoursesManagement.EventsHandlers.Lectures
{
    public interface ILectureResourcesRepo
    {
        Task<IList<AppResource>> GetLectureResources(string lectureId, CancellationToken token);
    }
}

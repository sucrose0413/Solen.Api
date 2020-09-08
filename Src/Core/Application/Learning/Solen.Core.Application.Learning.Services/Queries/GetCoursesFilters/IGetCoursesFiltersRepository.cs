using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Learning.Queries;

namespace Solen.Core.Application.Learning.Services.Queries
{
    public interface IGetCoursesFiltersRepository
    {
        Task<IList<LearnerCourseAuthorFilterDto>> GetCoursesAuthors(string organizationId, CancellationToken token);
    }
}
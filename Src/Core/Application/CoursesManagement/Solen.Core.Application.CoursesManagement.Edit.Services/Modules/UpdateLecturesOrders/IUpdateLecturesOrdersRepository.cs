using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Modules
{
    public interface IUpdateLecturesOrdersRepository
    {
        Task<List<Lecture>> GetModuleLectures(string moduleId, CancellationToken token);
        void UpdateLectures(IEnumerable<Lecture> lectures);
    }
}
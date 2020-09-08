using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Modules.Commands
{
    public interface IUpdateLecturesOrdersService
    {
        Task<List<Lecture>> GetModuleLecturesFromRepo(string moduleId, CancellationToken token);
        void UpdateLecturesOrders(List<Lecture> lectures, IEnumerable<LectureOrderDto> lecturesNewOrders);
        void UpdateLecturesRepo(IEnumerable<Lecture> lectures);
    }
}
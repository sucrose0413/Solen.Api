using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Modules
{
    public class UpdateLecturesOrdersService : IUpdateLecturesOrdersService
    {
        private readonly IUpdateLecturesOrdersRepository _repo;

        public UpdateLecturesOrdersService(IUpdateLecturesOrdersRepository repo)
        {
            _repo = repo;
        }
        
        public async Task<List<Lecture>> GetModuleLecturesFromRepo(string moduleId, CancellationToken token)
        {
            return await _repo.GetModuleLectures(moduleId, token);
        }
        
        public void UpdateLecturesOrders(List<Lecture> lectures, IEnumerable<LectureOrderDto> lecturesNewOrders)
        {
            var lecturesOrdersArray = lecturesNewOrders as LectureOrderDto[] ?? lecturesNewOrders.ToArray();

            lectures.ForEach(x =>
            {
                var order = lecturesOrdersArray.Any(m => m.LectureId == x.Id)
                    ? lecturesOrdersArray.First(m => m.LectureId == x.Id).Order
                    : x.Order;

                x.UpdateOrder(order);
            });
        }
        
        public void UpdateLecturesRepo(IEnumerable<Lecture> lectures)
        {
            _repo.UpdateLectures(lectures);
        }

    }
}
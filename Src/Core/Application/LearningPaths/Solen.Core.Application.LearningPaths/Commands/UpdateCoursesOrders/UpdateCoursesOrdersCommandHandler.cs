using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class UpdateCoursesOrdersCommandHandler : IRequestHandler<UpdateCoursesOrdersCommand>
    {
        private readonly IUpdateCoursesOrdersService _service;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCoursesOrdersCommandHandler(IUpdateCoursesOrdersService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(UpdateCoursesOrdersCommand command, CancellationToken token)
        {
           var coursesToUpdateOrders = await _service.GetLearningPathCourses(command.LearningPathId, token);
           
           _service.UpdateCoursesOrders(coursesToUpdateOrders, command.CoursesOrders);
           
           _service.UpdateLearningPathCoursesRepo(coursesToUpdateOrders);

           await _unitOfWork.SaveAsync(token);
           
           return Unit.Value;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;


namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, CommandResponse>
    {
        private readonly IDeleteCourseService _service;
        private readonly ICoursesCommonService _commonService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public DeleteCourseCommandHandler(IDeleteCourseService service, ICoursesCommonService commonService,
            IUnitOfWork unitOfWork, IMediator mediator)
        {
            _service = service;
            _commonService = commonService;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<CommandResponse> Handle(DeleteCourseCommand command, CancellationToken token)
        {
            var courseToDelete = await _commonService.GetCourseFromRepo(command.CourseId, token);

            _service.RemoveCourseFromRepo(courseToDelete);

            await _unitOfWork.SaveAsync(token);

            await _mediator.Publish(new CourseDeletedEvent(courseToDelete.Id), token);

            return new CommandResponse {Value = courseToDelete.Id};
        }
        
    }
}
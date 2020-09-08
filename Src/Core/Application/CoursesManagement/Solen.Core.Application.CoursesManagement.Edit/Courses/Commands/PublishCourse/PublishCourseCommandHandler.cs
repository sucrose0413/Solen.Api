using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class PublishCourseCommandHandler : IRequestHandler<PublishCourseCommand>
    {
        private readonly IPublishCourseService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public PublishCourseCommandHandler(IPublishCourseService service, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(PublishCourseCommand command, CancellationToken token)
        {
            var courseToPublish = await _service.GetCourseWithDetailsFromRepo(command.CourseId, token);

            await _service.CheckCourseErrors(command.CourseId, token);

            _service.ChangeTheCourseStatusToPublished(courseToPublish);

            _service.UpdatePublicationDate(courseToPublish);

            _service.UpdateCourseRepo(courseToPublish);

            await _unitOfWork.SaveAsync(token);

            await _mediator.Publish(new CoursePublishedEventNotification(courseToPublish.Id), token);

            return Unit.Value;
        }
    }
}
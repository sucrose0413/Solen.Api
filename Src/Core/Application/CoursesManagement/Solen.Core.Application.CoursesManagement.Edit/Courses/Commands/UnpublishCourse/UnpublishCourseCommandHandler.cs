using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;


namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class UnpublishCourseCommandHandler : IRequestHandler<UnpublishCourseCommand>
    {
        private readonly IUnpublishCourseService _service;
        private readonly ICoursesCommonService _commonService;
        private readonly IUnitOfWork _unitOfWork;

        public UnpublishCourseCommandHandler(IUnpublishCourseService service, ICoursesCommonService commonService,
            IUnitOfWork unitOfWork)
        {
            _service = service;
            _commonService = commonService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UnpublishCourseCommand command, CancellationToken token)
        {
            var courseToUnpublish = await _commonService.GetCourseFromRepo(command.CourseId, token);

            _service.ChangeTheCourseStatusToUnpublished(courseToUnpublish);

            _commonService.UpdateCourseRepo(courseToUnpublish);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
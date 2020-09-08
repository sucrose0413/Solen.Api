using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class DraftCourseCommandHandler : IRequestHandler<DraftCourseCommand>
    {
        private readonly IDraftCourseService _service;
        private readonly ICoursesCommonService _commonService;
        private readonly IUnitOfWork _unitOfWork;
        

        public DraftCourseCommandHandler(IDraftCourseService service, ICoursesCommonService commonService,
            IUnitOfWork unitOfWork)
        {
            _service = service;
            _commonService = commonService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DraftCourseCommand command, CancellationToken token)
        {
            var courseToDraft = await _commonService.GetCourseFromRepo(command.CourseId, token);

            _service.ChangeTheCourseStatusToDraft(courseToDraft);

            _commonService.UpdateCourseRepo(courseToDraft);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
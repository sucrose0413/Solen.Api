using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUpdateCourseService _service;
        private readonly ICoursesCommonService _commonService;


        public UpdateCourseCommandHandler(IUpdateCourseService service, ICoursesCommonService commonService,
            IUnitOfWork unitOfWork)
        {
            _service = service;
            _commonService = commonService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCourseCommand command, CancellationToken token)
        {
            var courseToUpdate = await _commonService.GetCourseFromRepo(command.CourseId, token);

            _commonService.CheckCourseStatusForModification(courseToUpdate);

            _service.UpdateCourseTitle(courseToUpdate, command.Title);

            _service.UpdateCourseSubtitle(courseToUpdate, command.Subtitle);

            _service.UpdateCourseDescription(courseToUpdate, command.Description);

            await _service.RemoveCourseSkillsFromRepo(courseToUpdate.Id, token);

            _service.AddSkillsToCourse(courseToUpdate, command.CourseLearnedSkills);

            _commonService.UpdateCourseRepo(courseToUpdate);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
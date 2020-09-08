using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public class UpdateLectureCommandHandler : IRequestHandler<UpdateLectureCommand, Unit>
    {
        private readonly IUpdateLectureService _service;
        private readonly ILecturesCommonService _commonService;
        private readonly IUnitOfWork _unitOfWork;


        public UpdateLectureCommandHandler(IUpdateLectureService service, ILecturesCommonService commonService,
            IUnitOfWork unitOfWork)
        {
            _service = service;
            _commonService = commonService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateLectureCommand command, CancellationToken token)
        {
            var lectureToUpdate = await _commonService.GetLectureFromRepo(command.LectureId, token);

            _commonService.CheckCourseStatusForModification(lectureToUpdate);

            _service.UpdateLectureTitle(lectureToUpdate, command.Title);

            _service.UpdateLectureDuration(lectureToUpdate, command.Duration);

            if (lectureToUpdate is ArticleLecture article)
                _service.UpdateArticleContent(article, command.Content);

            _service.UpdateLectureRepo(lectureToUpdate);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public class UploadMediaCommandHandler : IRequestHandler<UploadMediaCommand, Unit>
    {
        private readonly IUploadMediaService _service;
        private readonly ILecturesCommonService _commonService;
        private readonly IUnitOfWork _unitOfWork;

        public UploadMediaCommandHandler(IUploadMediaService service, ILecturesCommonService commonService,
            IUnitOfWork unitOfWork)
        {
            _service = service;
            _commonService = commonService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UploadMediaCommand command, CancellationToken token)
        {
            var lectureToUpdate = await _commonService.GetLectureFromRepo(command.LectureId, token);

            _commonService.CheckCourseStatusForModification(lectureToUpdate);

            _service.CheckIfTheLectureIsMedia(lectureToUpdate);

            _service.CheckFileFormat(command.File);

            await _service.CheckOrganizationMaximumStorage(command.File, token);

            var mediaResource = _service.GenerateMediaResource(command.File);

            var uploadResource = _service.UploadResource(mediaResource);

            _service.CreateAppResource(uploadResource.ResourceId, mediaResource);

            await _service.CreateCourseResource(command.LectureId, uploadResource.ResourceId, token);

            _service.SetMediaUrl((MediaLecture) lectureToUpdate, uploadResource.Url);

            _service.UpdateLectureRepo(lectureToUpdate);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}
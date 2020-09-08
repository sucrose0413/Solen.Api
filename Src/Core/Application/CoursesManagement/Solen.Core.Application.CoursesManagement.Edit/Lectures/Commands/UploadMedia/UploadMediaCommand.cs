using MediatR;
using Solen.Core.Application.Common.Resources;


namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public class UploadMediaCommand : IRequest<Unit>
    {
        public UploadMediaCommand(string lectureId, string lectureType, IResourceFile file)
        {
            LectureId = lectureId;
            LectureType = lectureType;
            File = file;
        }

        public string LectureId { get; }
        public string LectureType { get; }
        public IResourceFile File { get; }
    }
}
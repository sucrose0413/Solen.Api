using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Resources;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public interface IUploadMediaService
    {
        void CheckIfTheLectureIsMedia(Lecture lecture);
        void CheckFileFormat(IResourceFile file);
        Task CheckOrganizationMaximumStorage(IResourceFile file, CancellationToken token);
        ResourceToCreate GenerateMediaResource(IResourceFile file);
        ResourceUploadResult UploadResource(ResourceToCreate resourceToCreate);
        void CreateAppResource(string resourceId, ResourceToCreate resourceToCreate);
        Task CreateCourseResource(string lectureId, string resourceId, CancellationToken token);
        void SetMediaUrl(MediaLecture media, string url);
        void UpdateLectureRepo(Lecture lecture);
    }
}
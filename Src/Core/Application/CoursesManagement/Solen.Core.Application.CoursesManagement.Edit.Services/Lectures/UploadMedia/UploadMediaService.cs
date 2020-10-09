using System;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Resources;
using Solen.Core.Application.Common.Subscription;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Application.Exceptions;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Resources.Enums.ResourceTypes;
using Solen.Core.Domain.Subscription.Entities;


namespace Solen.Core.Application.CoursesManagement.Edit.Services.Lectures
{
    public class UploadMediaService : IUploadMediaService
    {
        private readonly IUploadMediaRepository _repo;
        private readonly IOrganizationSubscriptionManager _subscriptionManager;
        private readonly IAppResourceManager _appResourceManager;

        public UploadMediaService(IUploadMediaRepository repo, IOrganizationSubscriptionManager subscriptionManager,
            IAppResourceManager appResourceManager)
        {
            _repo = repo;
            _subscriptionManager = subscriptionManager;
            _appResourceManager = appResourceManager;
        }

        public void CheckIfTheLectureIsMedia(Lecture lecture)
        {
            if (!lecture.LectureType.IsMediaLecture)
                throw new AppBusinessException($"The lecture ({lecture.Id}) is not a media");
        }

        public void CheckFileFormat(IResourceFile file)
        {
            if (!file.ContentType.Equals("video/mp4", StringComparison.OrdinalIgnoreCase))
                throw new UnsupportedFileFormatException();
        }

        public async Task CheckOrganizationMaximumStorage(IResourceFile file, CancellationToken token)
        {
            var subscription = await _subscriptionManager.GetOrganizationSubscriptionPlan(token);
            var currentStorage = await _subscriptionManager.GetOrganizationCurrentStorage(token);

            if (DoesFileSizeExceedMaximumStorageAllowed(file, currentStorage, subscription))
                throw new FileExceedsTotalMaximumStorageException();

            if (DoesFileSizeExceedMaximumFileSizeAllowed(file, subscription))
                throw new FileExceedsMaximumFileSizeException();
        }

        public ResourceToCreate GenerateMediaResource(IResourceFile file)
        {
            return new ResourceToCreate(file, VideoType.Instance);
        }

        public ResourceUploadResult UploadResource(ResourceToCreate resourceToCreate)
        {
            return _appResourceManager.UploadResource(resourceToCreate);
        }


        public void CreateAppResource(string resourceId, ResourceToCreate resourceToCreate)
        {
            var appResource = _appResourceManager.CreateAppResource(resourceId, resourceToCreate);

            _appResourceManager.AddAppResourceToRepo(appResource);
        }

        public async Task CreateCourseResource(string lectureId, string resourceId, CancellationToken token)
        {
            var moduleIdCourseId = await _repo.GetLectureModuleIdAndCourseId(lectureId, token);

            var courseResource = new CourseResource(moduleIdCourseId.CourseId, moduleIdCourseId.ModuleId, lectureId,
                resourceId);

            await _repo.AddCourseResource(courseResource, token);
        }

        public void SetMediaUrl(MediaLecture media, string url)
        {
            media.SetUrl(url);
        }

        public void UpdateLectureRepo(Lecture lecture)
        {
            _repo.UpdateLecture(lecture);
        }

        #region private methods

        private static bool DoesFileSizeExceedMaximumStorageAllowed(IResourceFile file, long currentStorage,
            SubscriptionPlan subscription)
        {
            return (file.Length + currentStorage) > subscription.MaxStorage;
        }

        private static bool DoesFileSizeExceedMaximumFileSizeAllowed(IResourceFile file, SubscriptionPlan subscription)
        {
            return file.Length > subscription.MaxFileSize;
        }

        #endregion
    }
}
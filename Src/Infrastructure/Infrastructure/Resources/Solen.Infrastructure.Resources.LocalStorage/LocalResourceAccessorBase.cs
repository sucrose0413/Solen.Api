using System;
using System.IO;
using Microsoft.Extensions.Options;
using Solen.Core.Application.Common.Resources;

namespace Solen.Infrastructure.Resources.LocalStorage
{
    public abstract class LocalResourceAccessorBase : ResourceAccessor
    {
        private readonly string _resourcesFolder;
        private string _fileName;
        private readonly string _appBaseUrl;

        protected LocalResourceAccessorBase(IOptions<LocalStorageSettings> config, string resourcesFolder)
        {
            _resourcesFolder = resourcesFolder;
            _appBaseUrl = config.Value.BaseUrl;
        }

        public override ResourceUploadResult Add(IResourceFile file)
        {
            _fileName = $"{Guid.NewGuid().ToString()}{file.FileExtension}";

            using var stream = new FileStream(FileFullPath, FileMode.Create);
            file.CopyTo(stream);

            return new ResourceUploadResult(_fileName, FileUrl);
        }

        public override bool Delete(string publicId)
        {
            _fileName = publicId;

            try
            {
                if (File.Exists(FileFullPath))
                    File.Delete(FileFullPath);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        #region Private Methods

        private string TargetFolderFullPath => Path.Combine(Directory.GetCurrentDirectory(), _resourcesFolder);
        private string FileFullPath => Path.Combine(TargetFolderFullPath, _fileName);
        private string FileUrl => $"{_appBaseUrl}/{_resourcesFolder}/{_fileName}";

        #endregion
    }
}
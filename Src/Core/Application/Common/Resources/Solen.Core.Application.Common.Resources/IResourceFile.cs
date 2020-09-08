using System.IO;

namespace Solen.Core.Application.Common.Resources
{
    public interface IResourceFile
    {
        string ContentType { get; }
        string FileExtension { get; }
        string FileName { get; }
        long Length { get; }
        string Name { get; }
        void CopyTo(Stream target);
        Stream OpenReadStream();
    }
}
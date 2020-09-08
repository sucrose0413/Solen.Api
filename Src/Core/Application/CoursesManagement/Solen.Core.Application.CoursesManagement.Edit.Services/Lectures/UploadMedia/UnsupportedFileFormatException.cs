using Solen.Core.Application.Exceptions;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Lectures
{
    public class UnsupportedFileFormatException : AppBusinessException
    {
        public UnsupportedFileFormatException() : base("Unsupported File Format")
        {
        }
    }
}

using Solen.Core.Application.Exceptions;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Lectures
{
    public class FileExceedsTotalMaximumStorageException : AppSubscriptionException
    {
        public FileExceedsTotalMaximumStorageException() : base("The file size exceeds the total maximum storage allowed")
        {
        }
    }
}
using Solen.Core.Application.Exceptions;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Lectures
{
    public class FileExceedsMaximumFileSizeException : AppSubscriptionException
    {
    public FileExceedsMaximumFileSizeException() : base("The file size exceeds the maximum file size allowed")
    {
    }
}
}

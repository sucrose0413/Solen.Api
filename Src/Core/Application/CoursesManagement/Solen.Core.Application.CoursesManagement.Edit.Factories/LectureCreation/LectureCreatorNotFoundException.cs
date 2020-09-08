using Solen.Core.Application.Exceptions;

namespace Solen.Core.Application.CoursesManagement.Edit.Factories.LectureCreation
{
    public class LectureCreatorNotFoundException : AppBusinessException
    {
        public LectureCreatorNotFoundException(string message) : base(message)
        {
        }
    }
}
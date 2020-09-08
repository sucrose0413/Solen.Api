using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Notifications;


namespace Solen.Core.Application.CoursesManagement.EventsHandlers.Courses
{
    public interface ISendNotificationsToCourseLearnersRepo
    {
        Task<CourseInfo> GetCourseInfo(string courseId, CancellationToken token);
        Task<IEnumerable<RecipientContactInfo>> GetCourseLearners(string courseId, CancellationToken token);
    }
}
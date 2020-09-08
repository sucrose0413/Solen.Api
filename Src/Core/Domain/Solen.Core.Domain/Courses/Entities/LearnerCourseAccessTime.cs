using System;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Domain.Courses.Entities
{
    public class LearnerCourseAccessTime
    {
        private LearnerCourseAccessTime()
        {
        }
        public LearnerCourseAccessTime(string learnerId, string courseId)
        {
            LearnerId = learnerId;
            CourseId = courseId;
            AccessDate = DateTime.Now;
        }
        
        public User Learner { get; private set; }
        public string LearnerId { get; private set; }
        public Course Course { get; private set; }
        public string CourseId { get; private set; }
        public DateTime AccessDate { get; private set; }

        public void UpdateAccessTime(DateTime accessDate)
        {
            AccessDate = accessDate;
        }
    }
}
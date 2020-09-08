using System;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Domain.Courses.Entities
{
    public class LearnerCompletedLecture
    {
        private LearnerCompletedLecture()
        {
        }
        
        public LearnerCompletedLecture(string learnerId, string lectureId)
        {
            LearnerId = learnerId;
            LectureId = lectureId;
            CompletionDate = DateTime.Now;
        }

        public User Learner { get; private set; }
        public string LearnerId { get; private set; }
        public Lecture Lecture { get; private set; }
        public string LectureId { get; private set; }

        public DateTime CompletionDate { get; private set; }
    }
}
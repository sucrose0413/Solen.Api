using System;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Domain.Courses.Entities
{
    public class LearnerLectureAccessHistory
    {
        private LearnerLectureAccessHistory()
        {
        }
        public LearnerLectureAccessHistory(string learnerId, string lectureId)
        {
            LearnerId = learnerId;
            LectureId = lectureId;
            AccessDate = DateTime.Now;
        }
        
        public int Id { get; private set; }
        public User Learner { get; private set; }
        public string LearnerId { get; private set; }
        public Lecture Lecture { get; private set; }
        public string LectureId { get; private set; }
        public DateTime AccessDate { get; private set; }
    }
}
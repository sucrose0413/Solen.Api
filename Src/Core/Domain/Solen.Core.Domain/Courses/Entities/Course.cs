using System;
using System.Collections.Generic;
using System.Linq;
using Solen.Core.Domain.Common;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Domain.Courses.Entities
{
    public class Course : AuditableEntity
    {
        private readonly List<LearningPathCourse> _courseLearningPaths = new List<LearningPathCourse>();
        private readonly List<CourseLearnedSkill> _courseLearnedSkills = new List<CourseLearnedSkill>();

        #region Constructors

        private Course()
        {
        }

        public Course(string title, string creatorId, DateTime creationDate)
        {
            Id = CourseNewId;
            Title = title;
            CreationDate = creationDate;
            CreatorId = creatorId;
            CourseStatusName = new DraftStatus().Name;
        }

        #endregion

        #region Properties

        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string CourseStatusName { get; private set; }
        public string CreatorId { get; private set; }
        public User Creator { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? PublicationDate { get; private set; }
        public IList<Module> Modules { get; private set; } = new List<Module>();
        public IList<CourseLearnedSkill> CourseLearnedSkills => _courseLearnedSkills.AsReadOnly();
        public IEnumerable<LearningPathCourse> CourseLearningPaths => _courseLearningPaths.AsReadOnly();

        public CourseStatus CourseStatus => Enumeration.FromName<CourseStatus>(CourseStatusName);

        public virtual bool IsEditable => !(CourseStatus is PublishedStatus);

        #endregion

        #region Public Methods

        public virtual void UpdateTitle(string title)
        {
            Title = title;
        }

        public virtual void UpdateSubtitle(string subtitle)
        {
            Subtitle = subtitle;
        }

        public virtual void UpdateDescription(string description)
        {
            Description = description;
        }

        public virtual void UpdatePublicationDate(DateTime publicationDate)
        {
            PublicationDate = publicationDate;
        }

        public virtual void ChangeCourseStatus(CourseStatus courseStatus)
        {
            CourseStatusName = courseStatus.Name;
        }

        public virtual void AddLearnedSkill(string skill)
        {
            if (skill == null)
                return;

            var contains = _courseLearnedSkills.Any(x => x.Name == skill);
            if (!contains)
                _courseLearnedSkills.Add(new CourseLearnedSkill(Id, skill));
        }

        public void AddLearningPath(LearningPathCourse learningPath)
        {
            if (!CourseLearningPaths.Contains(learningPath))
                _courseLearningPaths.Add(learningPath);
        }

        public void RemoveLearningPath(LearningPathCourse learningPath)
        {
            if (CourseLearningPaths.Contains(learningPath))
                _courseLearningPaths.Remove(learningPath);
        }

        #endregion

        #region Private Methods

        private static string CourseNewId => new Random().Next(0, 999999999).ToString("D9");

        #endregion
    }
}
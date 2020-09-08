using System;
using System.Collections.Generic;
using System.Linq;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Domain.Courses.Entities
{
    public class LearningPath
    {
        private readonly List<LearningPathCourse> _learningPathCourses = new List<LearningPathCourse>();

        #region Constructors

        private LearningPath()
        {
        }

        public LearningPath(string name, string organizationId, bool isDeletable = true)
        {
            Id = LearningPathNewId;
            Name = name;
            OrganizationId = organizationId;
            IsDeletable = isDeletable;
        }

        #endregion

        #region Properties

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string OrganizationId { get; private set; }
        public Organization Organization { get; private set; }
        public virtual bool IsDeletable { get; private set; }
        public IList<User> Learners { get; private set; } = new List<User>();
        public IEnumerable<LearningPathCourse> LearningPathCourses => _learningPathCourses.AsReadOnly();

        #endregion

        #region Public Methods

        public virtual void UpdateName(string name)
        {
            Name = name;
        }

        public virtual void UpdateDescription(string description)
        {
            Description = description;
        }

        public virtual void AddCourse(string courseId)
        {
            if (LearningPathCourses.All(x => x.CourseId != courseId))
                _learningPathCourses.Add(new LearningPathCourse(Id, courseId, NextLearningPathCourseOrder));
        }

        public void RemoveCourse(string courseId)
        {
            var course = LearningPathCourses.SingleOrDefault(x => x.CourseId == courseId);
            if (course != null)
                _learningPathCourses.Remove(course);
        }

        #endregion


        #region Private Methods

        private static string LearningPathNewId => new Random().Next(0, 999999999).ToString("D9");

        private int NextLearningPathCourseOrder => LearningPathCourses.Any()
            ? LearningPathCourses.Max(x => x.Order) + 1
            : 1;

        #endregion
    }
}
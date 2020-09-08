using System;
using System.Collections.Generic;
using System.Linq;
using Solen.Core.Domain.Courses.Enums;


namespace Solen.Core.Domain.Courses.Entities
{
    public class Module
    {
        #region Constructors

        private Module()
        {
        }

        public Module(string name, string courseId, int order)
        {
            Id = ModuleNewId;
            Name = name;
            CourseId = courseId;
            Order = order;
        }
        
        public Module(string name, Course course, int order)
        {
            Id = ModuleNewId;
            Name = name;
            CourseId = course.Id;
            Course = course;
            Order = order;
        }

        #endregion

        #region Properties

        public string Id { get; private set; }
        public string Name { get; private set; }
        public int Order { get; private set; }
        public string CourseId { get; private set; }
        public Course Course { get; private set; }

        public IList<Lecture> Lectures { get; private set; } = new List<Lecture>();

        #endregion

        #region Public Methods

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateOrder(int order)
        {
            Order = order;
        }

        #endregion

        #region Private Methods

        private static string ModuleNewId => new Random().Next(0, 999999999).ToString("D9");        

        #endregion
    }
}
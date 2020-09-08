using System;
using Solen.Core.Domain.Common;
using Solen.Core.Domain.Courses.Enums.LectureTypes;

namespace Solen.Core.Domain.Courses.Entities
{
    public abstract class Lecture : AuditableEntity
    {
        #region Constructors

        protected Lecture(string title, string moduleId, LectureType lectureType, int order)
        {
            Id = LectureNewId;
            Title = title;
            ModuleId = moduleId;
            LectureTypeName = lectureType.Name;
            Order = order;
        }
        
        protected Lecture(string title, Module module, LectureType lectureType, int order)
        {
            Id = LectureNewId;
            Title = title;
            ModuleId = module.Id;
            Module = module;
            LectureTypeName = lectureType.Name;
            Order = order;
        }

        #endregion

        #region Properties

        public string Id { get; private set; }
        public string Title { get; private set; }
        public int Order { get; private set; }
        public int Duration { get; private set; }
        public string LectureTypeName { get; private set; }
        public string ModuleId { get; private set; }
        public Module Module { get; private set; }

        #endregion


        #region Public Methods

        public LectureType LectureType => Enumeration.FromName<LectureType>(LectureTypeName);

        public void UpdateTitle(string title)
        {
            Title = title;
        }

        public void UpdateDuration(int duration)
        {
            Duration = duration;
        }

        public void UpdateOrder(int order)
        {
            Order = order;
        }

        #endregion

        #region Private Methods

        private static string LectureNewId => new Random().Next(0, 999999999).ToString("D9");

        #endregion
    }
}
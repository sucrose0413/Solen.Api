using System.Collections.Generic;
using System.Linq;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Application.CoursesManagement.Edit.Services.Lectures;
using Solen.Core.Domain.Common;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.LectureTypes;

namespace Solen.Core.Application.CoursesManagement.Edit.Factories.LectureCreation
{
    public class LectureCreatorFactory : ILectureCreatorFactory
    {
        private readonly IList<ILectureCreator> _lectureCreators;

        public LectureCreatorFactory(IList<ILectureCreator> lectureCreators)
        {
            _lectureCreators = lectureCreators ?? new List<ILectureCreator>();
        }

        public Lecture Create(CreateLectureCommand command)
        {
            var lectureType = Enumeration.FromName<LectureType>(command.LectureType);

            var lectureCreator = _lectureCreators.FirstOrDefault(x => x.LectureType.Name == lectureType.Name);
            if (lectureCreator == null)
                throw new LectureCreatorNotFoundException(nameof(lectureCreator));
            
            return lectureCreator.Create(command);
        }

    }
}
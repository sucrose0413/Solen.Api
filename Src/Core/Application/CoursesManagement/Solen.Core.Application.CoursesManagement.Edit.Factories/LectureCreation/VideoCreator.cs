using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.LectureTypes;
using VideoLecture = Solen.Core.Domain.Courses.Enums.LectureTypes.VideoLecture;

namespace Solen.Core.Application.CoursesManagement.Edit.Factories.LectureCreation
{
    public class VideoCreator : ILectureCreator
    {
        public Lecture Create(CreateLectureCommand command)
        {
            return new Domain.Courses.Entities.VideoLecture(command.Title, command.ModuleId, command.Order);
        }

        public LectureType LectureType => new VideoLecture();
    }
}
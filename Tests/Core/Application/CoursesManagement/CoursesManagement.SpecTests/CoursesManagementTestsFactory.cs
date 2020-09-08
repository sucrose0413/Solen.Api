using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen;
using Solen.Core.Domain.Courses.Entities;
using Solen.SpecTests;

namespace CoursesManagement.SpecTests
{
    public class CoursesManagementTestsFactory : BaseWebApplicationFactory<Startup>
    {
        public void CreateCourse(Course course)
        {
            Context.Courses.Add(course);
            Context.SaveChanges();
        }

        public async Task<Course> GetCourseById(string courseId)
        {
            return await Context.Courses
                .AsNoTracking()
                .Include(x => x.CourseLearnedSkills)
                .Include(x => x.CourseLearningPaths)
                .SingleOrDefaultAsync(x => x.Id == courseId);
        }

        public async Task<Module> GetModuleById(string moduleId)
        {
            return await Context.Modules.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == moduleId);
        }

        public async Task<Lecture> GetLectureById(string lectureId)
        {
            return await Context.Lectures.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == lectureId);
        }

        public void CreateModule(Module module)
        {
            Context.Modules.Add(module);
            Context.SaveChanges();
        }

        public void CreateLecture(Lecture lecture)
        {
            Context.Lectures.Add(lecture);
            Context.SaveChanges();
        }
    }
}
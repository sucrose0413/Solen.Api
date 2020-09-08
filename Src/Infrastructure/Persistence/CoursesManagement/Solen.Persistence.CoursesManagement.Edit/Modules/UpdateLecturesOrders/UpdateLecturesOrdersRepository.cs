using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Edit.Services.Modules;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.Modules
{
    public class UpdateLecturesOrdersRepository : IUpdateLecturesOrdersRepository
    {
        private readonly SolenDbContext _context;

        public UpdateLecturesOrdersRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<List<Lecture>> GetModuleLectures(string moduleId, CancellationToken token)
        {
            return await _context.Lectures
                .Where(l => l.ModuleId == moduleId)
                .AsNoTracking()
                .ToListAsync(token);
        }

        public void UpdateLectures(IEnumerable<Lecture> lectures)
        {
            _context.Lectures.UpdateRange(lectures);
        }
    }
}
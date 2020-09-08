using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Common;
using Solen.Core.Application.CoursesManagement.Services.Modules;
using Solen.Core.Domain.Courses.Entities;


namespace Solen.Persistence.CoursesManagement.Modules
{
    public class GetModuleRepository : IGetModuleRepository
    {
        private readonly SolenDbContext _context;

        public GetModuleRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<ModuleDto> GetModule(string moduleId, string organizationId, CancellationToken token)
        {
            return await _context.Modules
                .Where(m => m.Id == moduleId && m.Course.Creator.OrganizationId == organizationId)
                .Select(MapModule())
                .SingleOrDefaultAsync(token);
        }

        #region Private Methods

        private static Expression<Func<Module, ModuleDto>> MapModule()
        {
            return x => new ModuleDto
            {
                Id = x.Id,
                Name = x.Name,
                Order = x.Order,
                Duration = x.Lectures.Sum(l => l.Duration)
            };
        }

        #endregion
    }
}
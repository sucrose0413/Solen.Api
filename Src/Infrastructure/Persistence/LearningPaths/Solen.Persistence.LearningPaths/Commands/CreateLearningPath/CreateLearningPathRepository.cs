using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.LearningPaths.Services.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.LearningPaths.Commands
{
    public class CreateLearningPathRepository : ICreateLearningPathRepository
    {
        private readonly SolenDbContext _context;

        public CreateLearningPathRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task AddLearningPath(LearningPath learningPath, CancellationToken token)
        {
            await _context.LearningPaths.AddAsync(learningPath, token);
        }
    }
}

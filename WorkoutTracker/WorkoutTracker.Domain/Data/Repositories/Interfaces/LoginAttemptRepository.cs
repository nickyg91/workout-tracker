using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Domain.Data.Contexts;
using WorkoutTracker.Domain.Data.Entities;

namespace WorkoutTracker.Domain.Data.Repositories.Interfaces
{
    public class LoginAttemptRepository : ILoginAttemptRepository
    {
        private readonly WorkoutTrackerContext _context;
        public LoginAttemptRepository(WorkoutTrackerContext context)
        {
            _context = context;
        }
        public async Task<bool> AddLoginAttemptAsync(LoginAttempt attempt)
        {
            await _context.LoginAttempts.AddAsync(attempt);
            await _context.SaveChangesAsync();
            return attempt.Id > 0;
        }

        public async Task<LoginAttempt> GetLastLoginAttemptForUserAsync(int userId)
        {
            return await _context.LoginAttempts.Where(x => x.UserId == userId).OrderByDescending(x => x.LastLogonAttemptUtc)
                .FirstOrDefaultAsync();
        }
    }
}

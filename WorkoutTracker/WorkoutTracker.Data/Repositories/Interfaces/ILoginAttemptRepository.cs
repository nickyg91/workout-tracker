using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutTracker.Data.Entities;

namespace WorkoutTracker.Data.Repositories.Interfaces
{
    public interface ILoginAttemptRepository
    {
        Task<bool> AddLoginAttemptAsync(LoginAttempt attempt);
        Task<LoginAttempt> GetLastLoginAttemptForUserAsync(int userId);
    }
}

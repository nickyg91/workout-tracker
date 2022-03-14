using System.Threading.Tasks;
using WorkoutTracker.Domain.Data.Entities;

namespace WorkoutTracker.Domain.Data.Repositories.Interfaces
{
    public interface ILoginAttemptRepository
    {
        Task<bool> AddLoginAttemptAsync(LoginAttempt attempt);
        Task<LoginAttempt> GetLastLoginAttemptForUserAsync(int userId);
    }
}

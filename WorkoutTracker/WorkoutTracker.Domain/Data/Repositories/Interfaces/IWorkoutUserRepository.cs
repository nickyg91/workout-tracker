using System.Threading.Tasks;
using WorkoutTracker.Domain.Data.Entities;

namespace WorkoutTracker.Domain.Data.Repositories.Interfaces
{
    public interface IWorkoutUserRepository
    {
        Task<WorkoutUser> CreateWorkoutUserAsync(WorkoutUser user);
        Task<bool> DisableAccountAsync(int workoutUserId);
        Task<bool> EnableAccountAsync(int workoutUserId);
        Task<WorkoutUser> UpdateWorkoutUserAsync(WorkoutUser user);
        Task<WorkoutUser> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<WorkoutUser> GetUserByEmail(string email);
        Task<bool> CheckIfUsernameExists(string username);
        Task<bool> CheckIfAccountExists(string email);
        Task<WorkoutUser> GetUserById(int id);
    }
}

using System.Threading.Tasks;
using WorkoutTracker.Domain.Data.Entities;

namespace WorkoutTracker.Domain
{
    public interface IAuthenticationRepository
    {
        Task<WorkoutUser> CreateAccount(WorkoutUser userToCreate);
        Task<WorkoutUser> Authenticate(string email, string password);
        Task<WorkoutUser> GetWorkoutUserById(int id);
        Task<bool> CheckIfUserAlreadyExistsForEmail(string email);
        Task<bool> CheckIfUsernameAlreadyTaken(string username);
    }
}

using System;
using System.Threading.Tasks;
using WorkoutTracker.Data.Repositories.Interfaces;
using WorkoutTracker.Dto.Dtos;
using WorkoutTracker.Dto.Interfaces;
using WorkoutUserDbEntity = WorkoutTracker.Data.Entities.WorkoutUser;

namespace WorkoutTracker.Domain
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IWorkoutUserRepository _workoutUserRepository;
        public AuthenticationRepository(IWorkoutUserRepository workoutUserRepository)
        {
            _workoutUserRepository = workoutUserRepository;
        }
        public async Task<WorkoutUser> CreateAccount(WorkoutUser userToCreate)
        {
            var userToInsert = new WorkoutUserDbEntity
            {
                Email = userToCreate.Email,
                Password = userToCreate.Password,
                BirthDate = userToCreate.BirthDate,
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
                TargetWeight = userToCreate.TargetWeight,
                Username = userToCreate.Username
            };
            var insertedUser = await _workoutUserRepository.CreateWorkoutUserAsync(userToInsert);
            return new WorkoutUser
            {
                Email = insertedUser.Email,
                Password = insertedUser.Password,
                BirthDate = insertedUser.BirthDate,
                FirstName = insertedUser.FirstName,
                LastName = insertedUser.LastName,
                TargetWeight = insertedUser.TargetWeight,
                Username = insertedUser.Username,
                Id = insertedUser.Id
            };
        }

        public async Task<WorkoutUser> Authenticate(string username, string password)
        {
            var authenticatedUser =
                await _workoutUserRepository.GetUserByUsernameOrEmailAndPasswordAsync(username, password);
            if (authenticatedUser == null)
            {
                return null;
            }
        }
    }
}

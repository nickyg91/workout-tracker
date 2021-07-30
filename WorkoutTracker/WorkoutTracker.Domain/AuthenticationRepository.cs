using System;
using System.Threading.Tasks;
using WorkoutTracker.Data.Entities;
using WorkoutTracker.Data.Repositories.Interfaces;
using WorkoutUser = WorkoutTracker.Dto.Dtos.WorkoutUser;
using WorkoutUserDbEntity = WorkoutTracker.Data.Entities.WorkoutUser;

namespace WorkoutTracker.Domain
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IWorkoutUserRepository _workoutUserRepository;
        private readonly ILoginAttemptRepository _loginAttemptRepository;
        public AuthenticationRepository(IWorkoutUserRepository workoutUserRepository, ILoginAttemptRepository loginAttemptRepository)
        {
            _workoutUserRepository = workoutUserRepository;
            _loginAttemptRepository = loginAttemptRepository;
        }
        public async Task<WorkoutUser> CreateAccount(WorkoutUser userToCreate)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userToCreate.Password);
            var userToInsert = new WorkoutUserDbEntity
            {
                Email = userToCreate.Email,
                Password = hashedPassword,
                BirthDate = userToCreate.BirthDate,
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
                TargetWeight = userToCreate.TargetWeight,
                Username = userToCreate.Username,
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

        public async Task<WorkoutUser> Authenticate(string email, string password)
        {
            var authenticatedUser =
                await _workoutUserRepository.GetUserByEmailAndPasswordAsync(email, password);
            var userAttemptingToLogIn = _workoutUserRepository.GetUserByEmail(email);
            if (userAttemptingToLogIn == null)
            {
                return null;
            }

            LoginAttempt loginAttempt;
            if (authenticatedUser == null)
            {
                loginAttempt = new LoginAttempt
                {
                    IsSuccessful = false,
                    LastLogonAttemptUtc = DateTime.UtcNow,
                    UserId = userAttemptingToLogIn.Id
                };
                await _loginAttemptRepository.AddLoginAttemptAsync(loginAttempt);
                return null;
            }
            loginAttempt = new LoginAttempt
            {
                IsSuccessful = true,
                LastLogonAttemptUtc = DateTime.UtcNow,
                UserId = userAttemptingToLogIn.Id
            };
            await _loginAttemptRepository.AddLoginAttemptAsync(loginAttempt);
            return new WorkoutUser
            {
                BirthDate = authenticatedUser.BirthDate,
                Email = authenticatedUser.Email,
                FirstName = authenticatedUser.FirstName,
                LastName = authenticatedUser.LastName,
                Id = authenticatedUser.Id,
                TargetWeight = authenticatedUser.TargetWeight,
                Username = authenticatedUser.Username,
            };
        }

        public async Task<bool> CheckIfUserAlreadyExistsForEmail(string email)
        {
            return await _workoutUserRepository.CheckIfAccountExists(email);
        }

        public async Task<bool> CheckIfUsernameAlreadyTaken(string username)
        {
            return await _workoutUserRepository.CheckIfUsernameExists(username);
        }
    }
}

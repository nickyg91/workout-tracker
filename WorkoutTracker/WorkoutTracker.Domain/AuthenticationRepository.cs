using System;
using System.Threading.Tasks;
using WorkoutTracker.Domain.Data.Entities;
using WorkoutTracker.Domain.Data.Repositories.Interfaces;

namespace WorkoutTracker.Domain
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IWorkoutUserRepository _workoutUserRepository;
        private readonly ILoginAttemptRepository _loginAttemptRepository;
        private readonly IEmailService _emailService;
        public AuthenticationRepository(IWorkoutUserRepository workoutUserRepository, ILoginAttemptRepository loginAttemptRepository, IEmailService emailService)
        {
            _emailService = emailService;
            _workoutUserRepository = workoutUserRepository;
            _loginAttemptRepository = loginAttemptRepository;
        }
        public async Task<WorkoutUser> CreateAccount(WorkoutUser userToCreate)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userToCreate.Password);
            var userToInsert = new WorkoutUser
            {
                Email = userToCreate.Email,
                Password = hashedPassword,
                BirthDate = userToCreate.BirthDate,
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
                TargetWeight = userToCreate.TargetWeight,
                Username = userToCreate.Username,
                ValidationToken = Guid.NewGuid(),
            };
            var insertedUser = await _workoutUserRepository.CreateWorkoutUserAsync(userToInsert);
            _emailService.SendAccountVerificationEmail(insertedUser);
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
            var userAttemptingToLogIn = await _workoutUserRepository.GetUserByEmail(email);
            if (userAttemptingToLogIn == null)
            {
                return null;
            }

            LoginAttempt loginAttempt;
            if (!BCrypt.Net.BCrypt.Verify(password, userAttemptingToLogIn.Password))
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
                BirthDate = userAttemptingToLogIn.BirthDate,
                Email = userAttemptingToLogIn.Email,
                FirstName = userAttemptingToLogIn.FirstName,
                LastName = userAttemptingToLogIn.LastName,
                Id = userAttemptingToLogIn.Id,
                TargetWeight = userAttemptingToLogIn.TargetWeight,
                Username = userAttemptingToLogIn.Username,
            };
        }

        public async Task<WorkoutUser> GetWorkoutUserById(int id)
        {
            var workoutUser = await _workoutUserRepository.GetUserById(id);
            return new WorkoutUser
            {
                Email = workoutUser.Email,
                Id = workoutUser.Id,
                Username = workoutUser.Username,
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

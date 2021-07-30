using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data.Contexts;
using WorkoutTracker.Data.Entities;

namespace WorkoutTracker.Data.Repositories.Interfaces
{
    public class WorkoutUserRepository : IWorkoutUserRepository
    {
        private readonly WorkoutTrackerContext _ctx;

        public WorkoutUserRepository(WorkoutTrackerContext context)
        {
            _ctx = context;
        }

        public async Task<WorkoutUser> CreateWorkoutUserAsync(WorkoutUser user)
        {
            await _ctx.AddAsync(user);
            await _ctx.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DisableAccountAsync(int workoutUserId)
        {
            var workoutUser = await _ctx.WorkoutUsers.SingleOrDefaultAsync(x => x.Id == workoutUserId);
            workoutUser.AccountDeactivated = true;
            await _ctx.SaveChangesAsync();
            return workoutUser.AccountDeactivated;
        }

        public async Task<bool> EnableAccountAsync(int workoutUserId)
        {
            var workoutUser = await _ctx.WorkoutUsers.SingleOrDefaultAsync(x => x.Id == workoutUserId);
            workoutUser.AccountDeactivated = false;
            await _ctx.SaveChangesAsync();
            return workoutUser.AccountDeactivated;
        }

        public async Task<WorkoutUser> UpdateWorkoutUserAsync(WorkoutUser user)
        {
            await _ctx.SaveChangesAsync();
            return await _ctx.WorkoutUsers.SingleOrDefaultAsync(x => x.Id == user.Id);
        }

        public async Task<WorkoutUser> GetUserByEmailAndPasswordAsync(string userName, string password)
        {
            var user = await _ctx.WorkoutUsers.SingleOrDefaultAsync(x =>
                x.Email.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
                x.Password == password);
            return user;
        }

        public async Task<WorkoutUser> GetUserByEmail(string email)
        {
            var user = await _ctx.WorkoutUsers.SingleOrDefaultAsync(x =>
                x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            return user;
        }

        public async Task<bool> CheckIfUsernameExists(string username)
        {
            var user = await _ctx.WorkoutUsers.FirstOrDefaultAsync(x =>
                x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            return user != null;
        }

        public async Task<bool> CheckIfAccountExists(string email)
        {
            var user = await _ctx.WorkoutUsers.FirstOrDefaultAsync(x =>
                x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            return user != null;
        }
    }
}

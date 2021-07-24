﻿using System.Threading.Tasks;
using WorkoutTracker.Data.Entities;

namespace WorkoutTracker.Data.Repositories.Interfaces
{
    public interface IWorkoutUserRepository
    {
        Task<WorkoutUser> CreateWorkoutUserAsync(WorkoutUser user);
        Task<bool> DisableAccountAsync(int workoutUserId);
        Task<bool> EnableAccountAsync(int workoutUserId);
        Task<WorkoutUser> UpdateWorkoutUserAsync(WorkoutUser user);
        Task<WorkoutUser> GetUserByUsernameOrEmailAndPasswordAsync(string usernameOrEmail, string password);
    }
}

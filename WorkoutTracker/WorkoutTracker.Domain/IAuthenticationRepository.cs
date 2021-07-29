﻿using System.Threading.Tasks;
using WorkoutTracker.Dto.Dtos;

namespace WorkoutTracker.Domain
{
    public interface IAuthenticationRepository
    {
        Task<WorkoutUser> CreateAccount(WorkoutUser userToCreate);
        Task<WorkoutUser> Authenticate(string userName, string password);
    }
}
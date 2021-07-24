using System;

namespace WorkoutTracker.Dto.Interfaces
{
    public interface IWorkoutUser
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        DateTime BirthDate { get; set; } 
        string Password { get; set; }
        decimal TargetWeight { get; set; }
        string Username { get; set; }
    }
}

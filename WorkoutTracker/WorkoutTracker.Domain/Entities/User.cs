using System;
using WorkoutTracker.Domain.Entities.Interfaces;

namespace WorkoutTracker.Domain.Entities
{
    public class User : IWorkoutUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public decimal TargetWeight { get; set; }
        public string Username { get; set; }
    }
}

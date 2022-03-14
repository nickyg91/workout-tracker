using WorkoutTracker.Domain.Entities.Interfaces;

namespace WorkoutTracker.Api.Models
{
    public class AuthenticatedUserModel
    {
        public IWorkoutUser User { get; set; }
        public string Token { get; set; }
    }
}

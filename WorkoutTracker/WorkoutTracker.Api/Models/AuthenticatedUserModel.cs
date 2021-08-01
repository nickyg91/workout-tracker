using WorkoutUser =  WorkoutTracker.Dto.Dtos.WorkoutUser;

namespace WorkoutTracker.Api.Models
{
    public class AuthenticatedUserModel
    {
        public WorkoutUser User { get; set; }
        public string Token { get; set; }
    }
}

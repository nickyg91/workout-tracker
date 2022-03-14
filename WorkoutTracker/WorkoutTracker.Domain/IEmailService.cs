
using WorkoutTracker.Domain.Data.Entities;

namespace WorkoutTracker.Domain
{
    public interface IEmailService
    {
        void SendAccountVerificationEmail(WorkoutUser user);
    }
}

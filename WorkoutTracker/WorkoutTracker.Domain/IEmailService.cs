using System.Threading.Tasks;
using WorkoutTracker.Data.Entities;

namespace WorkoutTracker.Domain
{
    public interface IEmailService
    {
        void SendAccountVerificationEmail(WorkoutUser user);
    }
}

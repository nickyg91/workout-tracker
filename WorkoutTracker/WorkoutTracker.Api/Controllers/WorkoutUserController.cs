
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Domain.Data.Contexts;

namespace WorkoutTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutUserController : ControllerBase
    {
        private readonly WorkoutTrackerContext _db;
        public WorkoutUserController(WorkoutTrackerContext db)
        {
            _db = db;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutTracker.Data.Contexts;

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

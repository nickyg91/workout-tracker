using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Api.Models;
using WorkoutTracker.Domain;
using WorkoutTracker.Domain.Data.Entities;

namespace WorkoutTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationRepository _authRepo;
        public AccountController(IAuthenticationRepository authenticationRepository)
        {
            _authRepo = authenticationRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount(CreateUserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            var accountExists = await _authRepo.CheckIfUserAlreadyExistsForEmail(user.Email);
            if (accountExists)
            {
                return BadRequest("User account already exists.");
            }

            var usernameTaken = await _authRepo.CheckIfUsernameAlreadyTaken(user.Email);

            if (usernameTaken)
            {
                return BadRequest("Username has already been taken.");
            }

            var userToCreate = new WorkoutUser
            {
                BirthDate = user.BirthDate,
                Email = user.Email,
                FirstName = user.FirstName,
                Username = user.Username,
                LastName = user.LastName,
                Password = user.Password,
                TargetWeight = user.TargetWeight
            };
            var createdUser = await _authRepo.CreateAccount(userToCreate);
            return Ok(createdUser);
        }
    }
}

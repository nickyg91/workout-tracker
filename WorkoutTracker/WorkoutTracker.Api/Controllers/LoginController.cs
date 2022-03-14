using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WorkoutTracker.Api.Models;
using WorkoutTracker.Domain;
using WorkoutTracker.Domain.Configuration;
using WorkoutTracker.Domain.Entities.Interfaces;

namespace WorkoutTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationRepository _authService;
        private readonly JwtSettings _jwtSettings;
        public LoginController(IAuthenticationRepository authService, IOptions<JwtSettings> jwtSettings)
        {
            _authService = authService;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> LogIn(LoginModel model)
        {
            var authenticatedUser = await _authService.Authenticate(model.Email, model.Password);
            if (authenticatedUser == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new("id", authenticatedUser.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.ExpiresInDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(descriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new AuthenticatedUserModel
            {
                Token = tokenString,
                User = authenticatedUser as IWorkoutUser
            });
        }
    }
}

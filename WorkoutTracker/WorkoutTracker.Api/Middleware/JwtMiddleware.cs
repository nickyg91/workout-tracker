using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WorkoutTracker.Domain;
using WorkoutTracker.Domain.Configuration;

namespace WorkoutTracker.Api.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _jwtSettings;
        public JwtMiddleware(RequestDelegate next, IOptions<JwtSettings> jwtSettings)
        {
            _next = next;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task Invoke(HttpContext context, IAuthenticationRepository authRepo)
        {

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.FromSeconds(30),
                    }, out var validatedToken);

                    if (validatedToken != null)
                    {
                        var jwt = (JwtSecurityToken)validatedToken;
                        var userId = int.Parse(jwt.Claims.FirstOrDefault(x => x.Type == "id").Value);
                        var user = authRepo.GetWorkoutUserById(userId);
                        context.Items["AuthenticatedUser"] = user;
                    }
                }

                await _next(context);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

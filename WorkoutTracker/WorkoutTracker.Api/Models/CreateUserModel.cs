using System;
using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Api.Models
{
    public class CreateUserModel
    {
        [Required, MaxLength(256)]
        public string FirstName { get; set; }
        [Required, MaxLength(256)]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public decimal TargetWeight { get; set; }
        [Required]
        public string Username { get; set; }
    }
}

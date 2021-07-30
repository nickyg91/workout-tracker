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
        [Required, 
         RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\]).{8,64}$", 
             ErrorMessage = "Invalid Password: It must contain at least 8 characters, one lowercase, one uppercase, one special character.")]
        public string Password { get; set; }
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public decimal TargetWeight { get; set; }
        [Required]
        public string Username { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkoutTracker.Dto.Interfaces;

namespace WorkoutTracker.Data.Entities
{
    [Table("workout_user")]
    public class WorkoutUser : IWorkoutUser
    {
        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(256), Column("first_name")]
        public string FirstName { get; set; }
        [StringLength(256), Column("last_name")]
        public string LastName { get; set; }
        [StringLength(256), Column("email")]
        public string Email { get; set; }
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("target_weight", TypeName = "decimal(4,1)"), ]
        public decimal TargetWeight { get; set; }
        [Column("account_deactivated")]
        public bool AccountDeactivated { get; set; }
        [Column("user_name"), StringLength(256)]
        public string Username { get; set; }
        public List<LoginAttempt> LoginAttempts { get; set; }
    }
}

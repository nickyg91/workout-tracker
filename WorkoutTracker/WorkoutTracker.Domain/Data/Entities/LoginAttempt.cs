using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutTracker.Domain.Data.Entities
{
    [Table("login_attempts")]
    public class LoginAttempt
    {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("last_login_attempt_utc")]
        public DateTime LastLogonAttemptUtc { get; set; }
        [Column("is_successful")]
        public bool IsSuccessful { get; set; }
        public WorkoutUser User { get; set; }
    }
}

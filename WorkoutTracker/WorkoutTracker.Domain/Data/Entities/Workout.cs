using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WorkoutTracker.Domain.Data.Entities
{
    [Table("workout")]
    public class Workout
    {
        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("workout_date_utc")]
        public DateTime WorkoutDateUtc { get; set; }
        [Column("workout_user_id")]
        public int WorkoutUserId { get; set; }
        [ForeignKey("WorkoutUserId")]
        public WorkoutUser WorkoutUser { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}

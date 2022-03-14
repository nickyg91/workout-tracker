using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutTracker.Domain.Data.Entities
{
    [Table("exercise")]
    public class Exercise
    {
        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(256), Column("name")]
        public string Name { get; set; }
        [Column("weight")]
        public int? Weight { get; set; }
        [Column("sets")]
        public byte? Sets { get; set; }
        [Column("repetitions")]
        public byte? Repetitions { get; set; }
        [Column("workout_id")]
        public int WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public Workout Workout { get; set; }
    }
}

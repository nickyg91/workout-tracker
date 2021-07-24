using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data.Entities;

namespace WorkoutTracker.Data.Contexts
{
    public class WorkoutTrackerContext : DbContext
    {
        private readonly string _connectionString;
        public WorkoutTrackerContext(DbContextOptions<WorkoutTrackerContext> options) : base(options)
        {
        }

        public WorkoutTrackerContext(DbContextOptions options, string connectionString) :
            base(options)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("workout");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<WorkoutUser> WorkoutUsers { get; set; }
        public DbSet<LoginAttempt> LoginAttempts { get; set; }
    }
}

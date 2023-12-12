using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Models;

namespace WorkoutPlanner.Data
{
    public class WorkOutContext: DbContext
    {
        public WorkOutContext(DbContextOptions<WorkOutContext> options) : base(options) {}
        public DbSet<Workout> Workouts { get; set; }
    }
}

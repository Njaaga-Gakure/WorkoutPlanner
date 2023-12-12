namespace WorkoutPlanner.Models
{
    public class Workout
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 

        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty; 
        
        public bool Completed { get; set; }  = false;
    }
}

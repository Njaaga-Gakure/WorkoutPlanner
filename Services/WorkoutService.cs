using AutoMapper;
using WorkoutPlanner.Data;
using WorkoutPlanner.DTOs;
using WorkoutPlanner.Models;
using WorkoutPlanner.Services.IServices;

namespace WorkoutPlanner.Services
{
    public class WorkoutService : IWorkout
    {
        public bool CreateWorkOut(Workout workout, WorkOutContext context)
        {
            try
            { 
                   context.Workouts.Add(workout);
                   context.SaveChanges();
                   return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public List<Workout> GetAllWorkouts(WorkOutContext context)
        {
            try
            { 
                var workouts = context.Workouts;

                if (workouts == null || workouts.Count() == 0)
                    return new List<Workout>();
                
                return workouts.ToList();   
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Workout>(); 
            }      
        }

        public Workout? GetWorkoutById(Guid id, WorkOutContext context)
        {
            try
            {
                var workout = context.Workouts
                                .FirstOrDefault(workout => workout.Id == id);
                if (workout == null)
                {
                    return null;
                }
                return workout;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool UpdateWorkout(Guid id, IMapper mapper, AddWorkoutDTO updatedWorkout, WorkOutContext context)
        {
            try
            {
                  var workout = GetWorkoutById(id, context);
                if (workout != null)
                { 
                    mapper.Map(updatedWorkout, workout);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool DeleteWorkout(Guid id, WorkOutContext context)
        {
            try
            {
                var workout = GetWorkoutById(id, context);
                if (workout != null)
                { 
                   context.Workouts.Remove(workout);
                   context.SaveChanges();
                    return true; 
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}

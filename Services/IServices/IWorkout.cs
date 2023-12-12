using AutoMapper;
using WorkoutPlanner.Data;
using WorkoutPlanner.DTOs;
using WorkoutPlanner.Models;

namespace WorkoutPlanner.Services.IServices
{
    public interface IWorkout
    {
        bool CreateWorkOut(Workout workout, WorkOutContext context);
        List<Workout> GetAllWorkouts(WorkOutContext context);
        Workout? GetWorkoutById(Guid id, WorkOutContext context);
        bool UpdateWorkout(Guid id, IMapper mapper, AddWorkoutDTO updatedWorkout, WorkOutContext context);
        bool DeleteWorkout(Guid id, WorkOutContext context);
    }
}

using AutoMapper;
using WorkoutPlanner.DTOs;
using WorkoutPlanner.Models;

namespace WorkoutPlanner.Profiles
{
    public class WorkoutProfiles: Profile
    {
        public WorkoutProfiles()
        {
            CreateMap<AddWorkoutDTO, Workout>();
        }
    }
}

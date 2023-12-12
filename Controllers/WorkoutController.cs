using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.Data;
using WorkoutPlanner.DTOs;
using WorkoutPlanner.Models;
using WorkoutPlanner.Services;
using System.Net;
using AutoMapper;

namespace WorkoutPlanner.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly WorkoutService _workoutService;
        private readonly ResponseDTO _response;
        private readonly WorkOutContext _context;
        private readonly IMapper _mapper;   
        public WorkoutController(WorkOutContext context, IMapper mapper)
        {
            _workoutService = new WorkoutService();
            _response = new ResponseDTO();
            _context = context; 
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<ResponseDTO> AddWorkout(AddWorkoutDTO newWorkout)
        {
            if (string.IsNullOrWhiteSpace(newWorkout.Name) || string.IsNullOrWhiteSpace(newWorkout.Description)) 
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Message = "Please fill in all the fields";
                return BadRequest(_response);
            }
            var workout = _mapper.Map<Workout>(newWorkout);
            bool successfullyCreated = _workoutService.CreateWorkOut(workout, _context);
            if (!successfullyCreated) 
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Message = "Something went wrong. Try again later";
                return StatusCode(500, _response);
            }
            _response.Result = workout;
            _response.StatusCode = HttpStatusCode.Created;
            return Created($"/api/v1/{workout.Id}", _response);
        }

        [HttpGet]
        public ActionResult<ResponseDTO> GetAllWorkouts()
        {
            var workouts = _workoutService.GetAllWorkouts(_context);
            _response.Result = workouts; 
            return Ok(_response);    
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseDTO> GetSingleWorkout(Guid id)
        { 
            var workout = _workoutService.GetWorkoutById(id, _context);

            if (workout == null) 
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "workout not found";
                _response.Result = null;
                return NotFound(_response);
            }

            _response.Result = workout;
            return Ok(_response);
        }

        [HttpPatch("{id}")]
        public ActionResult<ResponseDTO> UpdateWorkoutDetails(AddWorkoutDTO updatedWorkout, Guid id)
        {
            if (updatedWorkout == null || string.IsNullOrWhiteSpace(updatedWorkout.Name) || string.IsNullOrWhiteSpace(updatedWorkout.Description)) 
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Message = "Please fill in all the fields";
                return BadRequest(_response);
            }
            
            var workout = _workoutService.UpdateWorkout(id, _mapper, updatedWorkout, _context);

            if (!workout)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "The workout you are trying to update doesn't exist :(";
                return NotFound(_response);
            }
             
            _response.Result = _workoutService.GetWorkoutById(id, _context);
            return Ok(_response);


        } 

        [HttpDelete("{id}")]
        public ActionResult<ResponseDTO> DeleteWorkout(Guid id)
        { 
            bool deleted = _workoutService.DeleteWorkout(id, _context);
            if (!deleted)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Message = "The workout you are trying to delete does not exist :(";
                return NotFound(_response);
            }
            _response.Result = "workout deleted successfully :)";
            return Ok(_response);
        }

        
    }
}

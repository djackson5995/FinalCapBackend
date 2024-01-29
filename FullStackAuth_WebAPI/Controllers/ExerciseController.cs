using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using System;
using System.Linq;
using System.Security.Claims;
using FullStackAuth_WebAPI.DataTransferObjects;

namespace FullStackAuth_WebAPI.Controllers
{
    [ApiController]
    [Route("api/exercises")]
    [Authorize] 
    public class ExerciseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExerciseController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetAllExercises()
        {
            try
       
            {
                string userId = User.FindFirstValue("id");
                var exercise = _context.Exercises.Where(e => e.UserId.Equals(userId));

                return Ok(exercise);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
        [HttpPost]
        public IActionResult CreateExercise([FromBody] Exercise data)
        {
            try
            {
                var userId = User.FindFirstValue("id");

              
                data.UserId = userId;

            
                _context.Exercises.Add(data);
                _context.SaveChanges();

             
                return CreatedAtAction(nameof(GetExercise), new { id = data.Id }, data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetExercise(int id)
        {
            try
            {

                var exercise = _context.Exercises.Find(id);

                if (exercise == null)
                {
                    return NotFound();
                }

               
                var exerciseDto = new ExerciseDto
                {
                    Id = exercise.Id,
                    Type = exercise.Type,
                    Weight = exercise.Weight,
                    Reps = exercise.Reps,
                    Sets = exercise.Sets
                };

                return Ok(exerciseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExercise(int id, [FromBody] Exercise updatedExercise)
        {
            try
            {
             
                string userId = User.FindFirstValue("id");

               
                var existingExercise = _context.Exercises.FirstOrDefault(e => e.Id == id && e.UserId == userId);

                if (existingExercise == null)
                {
                   
                    return NotFound();
                }

             
                existingExercise.Type = updatedExercise.Type;
                existingExercise.Weight = updatedExercise.Weight;
                existingExercise.Reps = updatedExercise.Reps;
                existingExercise.Sets = updatedExercise.Sets;

                _context.SaveChanges();

                return Ok(existingExercise);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExercise(int id)
        {
            try
            {
               
                string userId = User.FindFirstValue("id");

               
                var exerciseToDelete = _context.Exercises.FirstOrDefault(e => e.Id == id && e.UserId == userId);

                if (exerciseToDelete == null)
                {
                   
                    return NotFound();
                }

               
                _context.Exercises.Remove(exerciseToDelete);
                _context.SaveChanges();

                return Ok($"Exercise with ID {id} has been deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}

using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using System.Security.Claims;

namespace FullStackAuth_WebAPI.Controllers
{
    [ApiController]
    [Route("api/training-events")]
    [Authorize]
    public class TrainingEventController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainingEventController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllTrainingEvents()
        {
            try
            {
                string userId = User.FindFirstValue("id");
                var trainingEvents = _context.TrainingEvents.Where(t => t.UserId.Equals(userId));
                return Ok(trainingEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateTrainingEvent([FromBody] TrainingEvent data)
        {
            try
            {
                var userId = User.FindFirstValue("id");
                data.UserId = userId;
                _context.TrainingEvents.Add(data);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetTrainingEvent), new { id = data.Id }, data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTrainingEvent(int id)
        {
            try
            {
                var trainingEvent = _context.TrainingEvents.Find(id);
                if (trainingEvent == null)
                {
                    return NotFound();
                }
                return Ok(trainingEvent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTrainingEvent(int id, [FromBody] TrainingEvent updatedTrainingEvent)
        {
            try
            {
                string userId = User.FindFirstValue("id");
                var existingTrainingEvent = _context.TrainingEvents.FirstOrDefault(t => t.Id == id && t.UserId == userId);
                if (existingTrainingEvent == null)
                {
                    return NotFound();
                }
                existingTrainingEvent.Date = updatedTrainingEvent.Date;
               
                _context.SaveChanges();
                return Ok(existingTrainingEvent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTrainingEvent(int id)
        {
            try
            {
                string userId = User.FindFirstValue("id");
                var trainingEventToDelete = _context.TrainingEvents.FirstOrDefault(t => t.Id == id && t.UserId == userId);
                if (trainingEventToDelete == null)
                {
                    return NotFound();
                }
                _context.TrainingEvents.Remove(trainingEventToDelete);
                _context.SaveChanges();
                return Ok($"Training event with ID {id} has been deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

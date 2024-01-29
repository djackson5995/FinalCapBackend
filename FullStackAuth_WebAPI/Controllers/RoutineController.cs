using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using FullStackAuth_WebAPI.DataTransferObjects;
using System.Security.Claims;

namespace FullStackAuth_WebAPI.Controllers
{
    [ApiController]
    [Route("api/routines")]
    [Authorize]
    public class RoutineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoutineController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllRoutines()
        {
            try
            {
                string userId = User.FindFirstValue("id");
                var routines = _context.Routines.Where(r => r.UserId.Equals(userId));
                return Ok(routines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateRoutine([FromBody] Routine data)
        {
            try
            {
                var userId = User.FindFirstValue("id");
                data.UserId = userId;
                _context.Routines.Add(data);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetRoutine), new { id = data.Id }, data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetRoutine(int id)
        {
            try
            {
                var routine = _context.Routines.Find(id);
                if (routine == null)
                {
                    return NotFound();
                }
                return Ok(routine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoutine(int id, [FromBody] Routine updatedRoutine)
        {
            try
            {
                string userId = User.FindFirstValue("id");
                var existingRoutine = _context.Routines.FirstOrDefault(r => r.Id == id && r.UserId == userId);
                if (existingRoutine == null)
                {
                    return NotFound();
                }
                existingRoutine.Title = updatedRoutine.Title;
                existingRoutine.Description = updatedRoutine.Description;
                _context.SaveChanges();
                return Ok(existingRoutine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoutine(int id)
        {
            try
            {
                string userId = User.FindFirstValue("id");
                var routineToDelete = _context.Routines.FirstOrDefault(r => r.Id == id && r.UserId == userId);
                if (routineToDelete == null)
                {
                    return NotFound();
                }
                _context.Routines.Remove(routineToDelete);
                _context.SaveChanges();
                return Ok($"Routine with ID {id} has been deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

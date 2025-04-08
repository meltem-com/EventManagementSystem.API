using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Data.Context;
using EventManagementSystem.Data.Entities;
using EventManagementSystem.API.Filters;

namespace EventManagementSystem.API.Controllers
{
    // This controller manages the many-to-many relationship between Users and Events
    [Route("api/[controller]")]
    [ApiController]
    public class UserEventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Injects the application's database context
        public UserEventsController(AppDbContext context)
        {
            _context = context;
        }

        // This endpoint can only be accessed during a specific time range (09:00 - 21:00)
        [ServiceFilter(typeof(TimeAccessFilter))]
        [HttpGet("limited-access")]
        public IActionResult LimitedAccessEndpoint()
        {
            return Ok("✅ Access granted during allowed hours!");
        }

        // GET: api/UserEvents
        // Returns all UserEvent records from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEvent>>> GetUserEvents()
        {
            return await _context.UserEvents.ToListAsync();
        }

        // GET: api/UserEvents/5
        // Returns a specific UserEvent record by UserId
        [HttpGet("{id}")]
        public async Task<ActionResult<UserEvent>> GetUserEvent(int id)
        {
            var userEvent = await _context.UserEvents.FindAsync(id);

            if (userEvent == null)
            {
                return NotFound();
            }

            return userEvent;
        }

        // PUT: api/UserEvents/5
        // Updates an existing UserEvent entry
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserEvent(int id, UserEvent userEvent)
        {
            if (id != userEvent.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserEvents
        // Adds a new UserEvent record (User joins an Event)
        [HttpPost]
        public async Task<ActionResult<UserEvent>> PostUserEvent(UserEvent userEvent)
        {
            _context.UserEvents.Add(userEvent);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserEventExists(userEvent.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserEvent", new { id = userEvent.UserId }, userEvent);
        }

        // DELETE: api/UserEvents/5
        // Removes a UserEvent entry (User leaves an Event)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserEvent(int id)
        {
            var userEvent = await _context.UserEvents.FindAsync(id);
            if (userEvent == null)
            {
                return NotFound();
            }

            _context.UserEvents.Remove(userEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to check if a UserEvent with given UserId exists
        private bool UserEventExists(int id)
        {
            return _context.UserEvents.Any(e => e.UserId == id);
        }
    }
}

using APIWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("entity-service/{roomName}")]
        public async Task<IActionResult> GetEntityServicesByRoom(string roomName)
        {
            var entityServices = await _context.EntityServices
                .Where(es => es.RoomName == roomName)
                .Select(es => new
                {
                    es.ServiceID,
                    Service = es.Service.Name
                })
                .ToListAsync();

            return Ok(entityServices);
        }

        [HttpDelete("entity-service/{entityID}")]
        public async Task<IActionResult> DeleteEntityService(string entityID)
        {
            var entityService = await _context.EntityServices.FindAsync(entityID);
            if (entityService == null)
                return NotFound("Entity service not found");

            _context.EntityServices.Remove(entityService);
            await _context.SaveChangesAsync();

            return Ok("Entity service deleted successfully");
        }

        [HttpPost("notification")]
        public async Task<IActionResult> CreateNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return Ok("Notification created successfully");
        }

        [HttpGet("notification/{userId}")]
        public async Task<IActionResult> GetNotificationsByUser(string userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .ToListAsync();

            return Ok(notifications);
        }

        [HttpDelete("notification/{userId}")]
        public async Task<IActionResult> DeleteNotificationsByUser(string userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .ToListAsync();

            _context.Notifications.RemoveRange(notifications);
            await _context.SaveChangesAsync();

            return Ok("Notifications deleted successfully");
        }
    }
}
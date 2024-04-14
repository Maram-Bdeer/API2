using APIWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser(string email, string username, string password, string phone, string room)
        {
            var user = new IdentityUser
            {
                Email = email,
                UserName = username
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                _context.Users.Add(new User
                {
                    IdentityUser = user,
                    Phone = phone,
                    Room = room
                });
                await _context.SaveChangesAsync();

                return Ok("User added successfully");
            }
            else
            {
                return BadRequest(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        [HttpDelete("delete-user/{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("User not found");

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                // حذف المستخدم من قاعدة البيانات الخاصة بك
                _context.Users.Remove(_context.Users.Find(user.Id));
                await _context.SaveChangesAsync();

                return Ok("User deleted successfully");
            }
            else
            {
                return BadRequest(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users
                .Select(u => new { u.Id, u.UserName, u.Email })
                .ToListAsync();

            return Ok(users);
        }

        [HttpPost("add-room")]
        public async Task<IActionResult> AddRoom(string roomName, string adminName, string adminID, string time, int numUser, string roomType)
        {
            // إضافة الغرفة إلى قاعدة البيانات مع المعلومات المقدمة
            _context.Rooms.Add(new Room
            {
                Name = roomName,
                Admin = adminName,
                AdminID = adminID,
                Time = time,
                NumUsers = numUser,
                Type = roomType
            });
            await _context.SaveChangesAsync();

            return Ok("Room added successfully");
        }

        [HttpDelete("delete-room/{roomName}")]
        public async Task<IActionResult> DeleteRoom(string roomName)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Name == roomName);
            if (room == null)
                return NotFound("Room not found");

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return Ok("Room deleted successfully");
        }
    }
}
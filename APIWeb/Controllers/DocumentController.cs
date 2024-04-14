using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace APIWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DocumentController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("add-signature")]
        public async Task<IActionResult> AddSignature(string signature, string userID, string documentID, bool status)
        {
            var user = await _userManager.FindByIdAsync(userID);
            if (user == null)
                return NotFound("User not found");

            var document = await _context.Documents.FindAsync(documentID);
            if (document == null)
                return NotFound("Document not found");

            document.Signature = signature;
            document.Status = status;
            await _context.SaveChangesAsync();

            return Ok("Signature added successfully");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(string email, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("User not found");

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

            if (result.Succeeded)
                return Ok("Password reset successful");
            else
                return BadRequest(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        [HttpDelete("delete-request/{requestID}")]
        public async Task<IActionResult> DeleteRequest(string requestID)
        {
            var request = await _context.Requests.FindAsync(requestID);
            if (request == null)
                return NotFound("Request not found");

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return Ok("Request deleted successfully");
        }

        [HttpGet("get-request/{requestID}")]
        public async Task<IActionResult> GetRequest(string requestID)
        {
            var request = await _context.Requests
                .Include(r => r.User)
                .Include(r => r.Document)
                .FirstOrDefaultAsync(r => r.Id == requestID);

            if (request == null)
                return NotFound("Request not found");

            return Ok(request);
        }

        [HttpGet("search-user")]
        public async Task<IActionResult> SearchUser(string username)
        {
            var users = await _userManager.Users
                .Where(u => u.UserName.Contains(username))
                .Select(u => new { u.Id, u.UserName, u.Email })
                .ToListAsync();

            return Ok(users);
        }
    }
}

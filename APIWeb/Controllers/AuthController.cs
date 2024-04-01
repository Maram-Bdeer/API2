using APIWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoginRequestModel = APIWeb.Models.LoginRequest;
using RegisterRequestModel = APIWeb.Models.RegisterRequest;

namespace APIWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request">Login request object</param>
        /// <returns>Login response</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return NotFound("User not found");

            var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: false, lockoutOnFailure: false);

            if (signInResult.Succeeded)
                return Ok("Login successful");
            else
                return Unauthorized("Invalid credentials");
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="request">Register request object</param>
        /// <returns>Register response</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestModel request)
        {
            var user = new IdentityUser
            {
                Email = request.Email,
                UserName = request.Username
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
                return Ok("Registration successful");
            else
                return BadRequest(result.Errors);
        }

        /// <summary>
        /// Update account
        /// </summary>
        /// <param name="request">Update account request object</param>
        /// <returns>Update account response</returns>
        [HttpPost("update-account")]
        public async Task<IActionResult> UpdateAccount(UpdateAccountRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound("User not found");

            user.UserName = request.Name;
            // Update user photo if needed

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Ok("Account updated successfully");
            else
                return BadRequest(result.Errors);
        }
    }
}
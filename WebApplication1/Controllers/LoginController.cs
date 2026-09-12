using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Authenticates a user with their credentials
        /// </summary>
        /// <param name="request">Login credentials containing username and password</param>
        /// <returns>Authentication token on successful login</returns>
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                _logger.LogWarning("Login attempt with missing or invalid credentials");
                return BadRequest(new { message = "Username and password are required" });
            }

            // TODO: Replace with actual user validation against a database
            if (ValidateUser(request.Username, request.Password))
            {
                _logger.LogInformation("Successful login for user: {Username}", request.Username);
                var token = GenerateToken(request.Username);
                return Ok(new LoginResponse { Token = token, Username = request.Username });
            }

            _logger.LogWarning("Failed login attempt for user: {Username}", request.Username);
            return Unauthorized(new { message = "Invalid username or password" });
        }

        /// <summary>
        /// Validates user credentials (placeholder implementation)
        /// </summary>
        private bool ValidateUser(string username, string password)
        {
            // TODO: Implement actual user validation against database
            // This is a placeholder - in production, use proper authentication
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
        }

        /// <summary>
        /// Generates an authentication token (placeholder implementation)
        /// </summary>
        private string GenerateToken(string username)
        {
            // TODO: Implement JWT or similar token generation
            // This is a placeholder - in production, use proper token generation
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{username}:{DateTime.UtcNow.Ticks}"));
        }
    }

    /// <summary>
    /// Login request model
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// User's username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// Login response model
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Authentication token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Authenticated username
        /// </summary>
        public string Username { get; set; }
    }
}

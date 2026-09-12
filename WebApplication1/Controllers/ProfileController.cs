using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets the user profile by ID
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <returns>User profile information</returns>
        [HttpGet("{userId}")]
        public IActionResult GetProfile(int userId)
        {
            if (userId <= 0)
            {
                _logger.LogWarning("Invalid user ID requested: {UserId}", userId);
                return BadRequest(new { message = "Invalid user ID" });
            }

            // TODO: Replace with actual database lookup
            var profile = GetUserProfileFromDatabase(userId);
            if (profile == null)
            {
                _logger.LogWarning("Profile not found for user ID: {UserId}", userId);
                return NotFound(new { message = "Profile not found" });
            }

            _logger.LogInformation("Profile retrieved for user ID: {UserId}", userId);
            return Ok(profile);
        }

        /// <summary>
        /// Updates the user profile
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="request">Profile update data</param>
        /// <returns>Updated profile information</returns>
        [HttpPut("{userId}")]
        public IActionResult UpdateProfile(int userId, [FromBody] UpdateProfileRequest request)
        {
            if (userId <= 0)
            {
                _logger.LogWarning("Invalid user ID for update: {UserId}", userId);
                return BadRequest(new { message = "Invalid user ID" });
            }

            if (request == null || string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
            {
                _logger.LogWarning("Invalid profile data provided for user ID: {UserId}", userId);
                return BadRequest(new { message = "FirstName and LastName are required" });
            }

            // TODO: Replace with actual database update
            var updatedProfile = UpdateUserProfileInDatabase(userId, request);
            if (updatedProfile == null)
            {
                _logger.LogWarning("Failed to update profile for user ID: {UserId}", userId);
                return NotFound(new { message = "Profile not found" });
            }

            _logger.LogInformation("Profile updated successfully for user ID: {UserId}", userId);
            return Ok(updatedProfile);
        }

        /// <summary>
        /// Deletes a user profile
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <returns>Deletion status</returns>
        [HttpDelete("{userId}")]
        public IActionResult DeleteProfile(int userId)
        {
            if (userId <= 0)
            {
                _logger.LogWarning("Invalid user ID for deletion: {UserId}", userId);
                return BadRequest(new { message = "Invalid user ID" });
            }

            // TODO: Replace with actual database deletion
            bool deleted = DeleteUserProfileFromDatabase(userId);
            if (!deleted)
            {
                _logger.LogWarning("Failed to delete profile for user ID: {UserId}", userId);
                return NotFound(new { message = "Profile not found" });
            }

            _logger.LogInformation("Profile deleted successfully for user ID: {UserId}", userId);
            return Ok(new { message = "Profile deleted successfully" });
        }

        /// <summary>
        /// Gets all profiles (admin endpoint)
        /// </summary>
        /// <returns>List of all user profiles</returns>
        [HttpGet]
        public IActionResult GetAllProfiles()
        {
            _logger.LogInformation("Retrieving all profiles");
            var profiles = GetAllUserProfilesFromDatabase();
            return Ok(profiles);
        }

        private UserProfile GetUserProfileFromDatabase(int userId)
        {
            // TODO: Implement actual database lookup
            return new UserProfile
            {
                UserId = userId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+1-234-567-8900",
                DateOfBirth = new DateOnly(1990, 1, 15),
                CreatedDate = DateTime.UtcNow.AddDays(-30)
            };
        }

        private UserProfile UpdateUserProfileInDatabase(int userId, UpdateProfileRequest request)
        {
            // TODO: Implement actual database update
            return new UserProfile
            {
                UserId = userId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                CreatedDate = DateTime.UtcNow.AddDays(-30),
                UpdatedDate = DateTime.UtcNow
            };
        }

        private bool DeleteUserProfileFromDatabase(int userId)
        {
            // TODO: Implement actual database deletion
            return true;
        }

        private List<UserProfile> GetAllUserProfilesFromDatabase()
        {
            // TODO: Implement actual database lookup for all profiles
            return new List<UserProfile>
            {
                new UserProfile
                {
                    UserId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "+1-234-567-8900",
                    DateOfBirth = new DateOnly(1990, 1, 15),
                    CreatedDate = DateTime.UtcNow.AddDays(-30)
                },
                new UserProfile
                {
                    UserId = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "+1-234-567-8901",
                    DateOfBirth = new DateOnly(1992, 3, 20),
                    CreatedDate = DateTime.UtcNow.AddDays(-20)
                }
            };
        }
    }

    /// <summary>
    /// User profile model
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Unique user identifier
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User's email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// User's date of birth
        /// </summary>
        public DateOnly DateOfBirth { get; set; }

        /// <summary>
        /// Profile creation timestamp
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Profile last updated timestamp
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }

    /// <summary>
    /// Profile update request model
    /// </summary>
    public class UpdateProfileRequest
    {
        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User's email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// User's date of birth
        /// </summary>
        public DateOnly DateOfBirth { get; set; }
    }
}

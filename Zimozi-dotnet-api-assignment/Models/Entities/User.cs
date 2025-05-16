using Microsoft.EntityFrameworkCore;

namespace Zimozi_dotnet_api_assignment.Models.Entities
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

        /// <summary>
        /// User Role: Admin/User
        /// </summary>
        public required UserRole Role { get; set; }

        /// <summary>
        /// Navigable Tasks assigned to User
        /// </summary>
        public ICollection<UserTask> AssignedTasks { get; set; } = new List<UserTask>();

        // Comments not declared as navigable
    }
}

public enum UserRole
{
    Admin,
    User
}
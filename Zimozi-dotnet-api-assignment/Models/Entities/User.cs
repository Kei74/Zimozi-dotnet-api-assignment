namespace Zimozi_dotnet_api_assignment.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required UserRole Role { get; set; }

        public ICollection<UserTask>? Tasks { get; set; }
    }
}

public enum UserRole
{
    Admin,
    User
}
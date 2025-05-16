namespace Zimozi_dotnet_api_assignment.Models.Entities
{
    public class UserTask
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        /// <summary>
        /// Assigned User
        /// </summary>
        public required User AssignedUser { get; set; }
        public required Guid AssignedUserId { get; set; }

        /// <summary>
        /// Navigable Comments on Task
        /// </summary>
        public ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();
    }
}

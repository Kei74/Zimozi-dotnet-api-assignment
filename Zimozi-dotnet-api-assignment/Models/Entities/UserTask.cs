namespace Zimozi_dotnet_api_assignment.Models.Entities
{
    public class UserTask
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        public required User User { get; set; }

        public ICollection<TaskComment>? TaskComments { get; set; }
    }
}

namespace Zimozi_dotnet_api_assignment.Models.Entities
{
    public class TaskComment
    {
        public Guid Id { get; set; }
        public required string Message { get; set; }

        public required UserTask UserTask { get; set; }
    }
}

namespace Zimozi_dotnet_api_assignment.Models.DTO
{
    public class AddTaskDto
    {
        public required string UserName { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}

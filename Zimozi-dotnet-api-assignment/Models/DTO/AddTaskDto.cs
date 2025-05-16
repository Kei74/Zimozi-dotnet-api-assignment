namespace Zimozi_dotnet_api_assignment.Models.DTO
{
    public class AddTaskDto
    {
        /// <summary>
        /// Using Username in DTO instead of Id for simpler API call demonstrations
        /// </summary>
        public required string Username { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}

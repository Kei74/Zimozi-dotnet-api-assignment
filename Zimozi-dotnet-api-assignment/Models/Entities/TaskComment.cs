namespace Zimozi_dotnet_api_assignment.Models.Entities
{
    public class TaskComment
    {
        public Guid Id { get; set; }
        public required string Message { get; set; }

        /// <summary>
        /// Parent Task
        /// </summary>
        public required UserTask UserTask { get; set; }
        
        /// <summary>
        /// Foreign key of Task ID
        /// </summary>
        public required Guid TaskId { get; set; }

        /// <summary>
        /// Comment Author, nullable to accomodate for comments by deleted users
        /// </summary>
        public User? User {  get; set; }
        public Guid? UserId { get; set; }
    }
}

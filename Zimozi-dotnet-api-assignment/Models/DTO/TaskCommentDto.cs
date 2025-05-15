using Zimozi_dotnet_api_assignment.Models.Entities;

namespace Zimozi_dotnet_api_assignment.Models.DTO
{
    public class TaskCommentDto
    {
        public required Guid Id { get; set; }
        public required string Message { get; set; }
        public required Guid TaskId { get; set; }
        public Guid? UserId { get; set; }

        /*
        public TaskCommentDto(TaskComment comment)
        {
            this.Id = comment.Id;
            this.Message = comment.Message;
            this.TaskId = comment.TaskId;
            if(comment.UserId != null)
            {
                this.UserId = comment.UserId;
            }
        }
        */
    }
}

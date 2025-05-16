using Zimozi_dotnet_api_assignment.Models.Entities;

namespace Zimozi_dotnet_api_assignment.Models.DTO
{
    public class TaskDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        /// <summary>
        /// Using Username in DTO instead of Id for simpler API call demonstrations
        /// </summary>
        public required string AssignedUsername { get; set; }
        public ICollection<TaskCommentDto> Comments { get; set; } = [];

        /*
        public TaskDto(UserTask task, bool includeComments = false)
        {
            this.Id = task.Id;
            this.Name = task.Name;
            this.Description = task.Description;
            this.AssignedUserId = task.AssignedUser.Id;

            if (includeComments && task.TaskComments != null)
            {
                this.Comments = new List<TaskCommentDto>();
                foreach (TaskComment comment in task.TaskComments)
                {
                    TaskCommentDto taskCommentDto = new TaskCommentDto(comment);
                    // Comments.Add();
                }
            }
        }

        public TaskDto()
        {
        }
        */
    }
}

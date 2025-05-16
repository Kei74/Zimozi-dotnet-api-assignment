using Zimozi_dotnet_api_assignment.Models.Entities;

namespace Zimozi_dotnet_api_assignment.Repositories
{
    public interface ITaskCommentRepository
    {
        public List<TaskComment> GetByTaskId(Guid id);
    }
}

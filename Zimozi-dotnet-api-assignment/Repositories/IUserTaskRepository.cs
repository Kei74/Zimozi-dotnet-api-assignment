using Zimozi_dotnet_api_assignment.Models.Entities;

namespace Zimozi_dotnet_api_assignment.Repositories
{
    public interface IUserTaskRepository
    {
        public List<UserTask> GetAllWithUsers();
        public List<UserTask> GetAll();
        public UserTask? GetByIdWithUserAndComment(Guid id);
        public UserTask Create(UserTask task);
    }
}

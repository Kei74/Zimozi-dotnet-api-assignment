using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Zimozi_dotnet_api_assignment.data;
using Zimozi_dotnet_api_assignment.Models.Entities;

namespace Zimozi_dotnet_api_assignment.Repositories
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserTaskRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public UserTask Create(UserTask task)
        {
            _dbContext.UserTasks.Add(task);
            _dbContext.SaveChanges();
            return task;
        }

        public List<UserTask> GetAll()
        {
            return _dbContext.UserTasks.ToList();
        }

        public List<UserTask> GetAllWithUsers()
        {
            return _dbContext.UserTasks.Include(task => task.AssignedUser).ToList();
        }

        public UserTask? GetByIdWithUserAndComment(Guid id)
        {
            return _dbContext.UserTasks.Include(task => task.AssignedUser)
                    .Include(task => task.TaskComments)
                    .FirstOrDefault(task => task.Id == id);
        }
    }
}

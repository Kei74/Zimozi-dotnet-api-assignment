using Zimozi_dotnet_api_assignment.data;
using Zimozi_dotnet_api_assignment.Models.Entities;

namespace Zimozi_dotnet_api_assignment.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public User? GetById(Guid id)
        {
            return _dbContext.Users.SingleOrDefault(u => u.Id == id);
        }

        public User? GetByUsername(string username)
        {
            return _dbContext.Users.SingleOrDefault(u => u.Username == username);
        }
    }
}

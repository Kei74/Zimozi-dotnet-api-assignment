using Zimozi_dotnet_api_assignment.Models.Entities;

namespace Zimozi_dotnet_api_assignment.Repositories
{
    public interface IUserRepository
    {
        public User? GetById(Guid id);
        public User? GetByUsername(string username);
    }
}

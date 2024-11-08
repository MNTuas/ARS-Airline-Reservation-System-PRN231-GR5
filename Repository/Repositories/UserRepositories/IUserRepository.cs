using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;

namespace Repository.Repositories.UserRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> GetAllUserExceptAdmin();
        Task<User> GetUserById(string id);
        Task<User> GetUserByEmail(string email);
    }
}

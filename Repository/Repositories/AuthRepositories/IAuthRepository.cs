using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;

namespace Repository.Repositories.AuthRepositories
{
    public interface IAuthRepository : IGenericRepository<User>
    {
    }
}

using BusinessObjects.Models;
using DAO;
using Repository.Enums;

namespace Repository.Repositories.UserRepositories
{
    public class UserRepository : GenericDAO<User>, IUserRepository
    {
        public async Task<List<User>> GetAllUserExceptAdmin()
        {
            var list = await Get(u => !u.Role.Equals(UserRolesEnums.Admin.ToString()), includeProperties: "Rank");
            return list.ToList();
        }

        public async Task<User> GetUserById(string id)
        {
            return await GetSingle(u => u.Id.Equals(id), includeProperties: "Rank");
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await GetSingle(u => u.Email.ToLower().Equals(email.ToLower()), includeProperties: "Rank");
        }
    }
}

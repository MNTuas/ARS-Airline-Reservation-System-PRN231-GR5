using BusinessObjects.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.AuthRepositories
{
    public class AuthRepository : GenericDAO<User>, IAuthRepository
    {
    }
}

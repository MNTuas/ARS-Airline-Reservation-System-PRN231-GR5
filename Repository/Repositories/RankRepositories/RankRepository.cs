using BusinessObjects.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.RankRepositories
{
    public class RankRepository : GenericDAO<Rank>, IRankRepository
    {
        public async Task<List<Rank>> GetAllRankAsync()
        {
            var listRank = await Get();
            return listRank.ToList();
        }
        public async Task<Rank> GetRank(string id)
        {
            var rank = await GetSingle(r => r.Id.Equals(id));
            return rank;
        }

        public async Task<string> GetRankIdByName(string name)
        {
            var rank = await GetSingle(r => r.Type.ToLower().Equals(name.ToLower()));
            return rank.Id;
        }
    }
}

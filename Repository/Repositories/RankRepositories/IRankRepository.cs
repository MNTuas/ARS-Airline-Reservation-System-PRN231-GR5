using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;

namespace Repository.Repositories.RankRepositories
{
    public interface IRankRepository : IGenericRepository<Rank>
    {
        Task<List<Rank>> GetAllRankAsync();
        Task<Rank> GetRank(string id);
        Task<string> GetRankIdByName(string name);

    }
}

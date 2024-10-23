using BusinessObjects.Models;
using BusinessObjects.RequestModels.Rank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.RankServices
{
    public interface IRankService
    {
        Task<List<Rank>> GetAllRank();

        Task<bool> AddRank(AddRankRequest rank);
        Task<bool> RemoveRank(Guid id);
        Task<bool> UpdateRank(string id,UpdateRankRequest rank);
        Task<Rank> GetRank(string id);
    }
}

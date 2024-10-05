using BusinessObjects.Models;
using BusinessObjects.RequestModels.Rank;
using Repository.Repositories.RankRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.RankServices
{
    public class RankService : IRankService
    {
        private readonly IRankRepository _rankRepository;
        public RankService(IRankRepository rankRepository)
        {
            _rankRepository = rankRepository;
        }
        public async Task<bool> AddRank(AddRankRequest rank)
        {
            await _rankRepository.Insert(new Rank
            {
                Id = Guid.NewGuid().ToString(),
                Type = rank.Type,
                Description = rank.Description,
                Discount = rank.Discount,
            });
            return true;
        }

        public async Task<List<Rank>> GetAllRank()
        {
            var result = await _rankRepository.GetAllRankAsync();
            return result;
        }

        public async Task<Rank> GetRank(string id)
        {
            var rankExist = await _rankRepository.GetRank(id);
            return rankExist;
        }

        public Task<bool> RemoveRank(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateRank(string id, UpdateRankRequest rank)
        {
            var rankExist = await _rankRepository.GetRank(id);
            if (rankExist == null)
            {
                return false;
            }
            rankExist.Discount = rank.Discount;
            rankExist.Description = rank.Description;
            rankExist.Type = rank.Type;
            await _rankRepository.Update(rankExist);
            return true;
        }
    }
}

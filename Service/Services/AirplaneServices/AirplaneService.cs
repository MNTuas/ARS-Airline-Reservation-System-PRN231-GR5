using BusinessObjects.Models;
using BusinessObjects.RequestModels.Airplane;
using BusinessObjects.RequestModels.Rank;
using Repository.Repositories.AirlineRepositories;
using Repository.Repositories.AirplaneRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Service.Services.AirplaneServices
{
    public class AirplaneService : IAirplaneService
    {
        private readonly IAirplaneRepository _airlplaneRepository;
        public AirplaneService(IAirplaneRepository airplaneRepository)
        {
            _airlplaneRepository = airplaneRepository;
        }

        public async Task<bool> AddAirplane(AddAirplaneRequest airplane)
        {
            await _airlplaneRepository.Insert(new Airplane
            {
                Id = Guid.NewGuid().ToString(),
                Code = airplane.Code,
                Type = airplane.Type,
                AvailableSeat = airplane.AvailableSeat,
                AirlinesId = airplane.AirlinesId,
                Status = airplane.Status
            });
            return true;
        }

        public async Task<Airplane> GetAirlane(string id)
        {
            var rankExist = await _airlplaneRepository.GetAirplane(id);
            return rankExist;
        }

        public async Task<List<Airplane>> GetAllAirplane()
        {
            var result = await _airlplaneRepository.GetAllAirplaneAsync();
            return result.ToList();
        }

        public Task<bool> RemoveAirplane(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAirplane(string id, UpdateAirplaneRequest updateAirplane)
        {
            var airlaneExist = await _airlplaneRepository.GetAirplane(id);
            if (airlaneExist == null)
            {
                return false;
            }
            airlaneExist.Code = updateAirplane.Code;
            airlaneExist.Type = updateAirplane.Type;
            airlaneExist.Status = updateAirplane.Status;
            airlaneExist.AvailableSeat = updateAirplane.AvailableSeat;
            airlaneExist.AirlinesId = airlaneExist.AirlinesId;
            await _airlplaneRepository.Update(airlaneExist);
            return true;
        }

        
    }
}

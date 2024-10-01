using BusinessObjects.Models;
using BusinessObjects.ResponseModels;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.AirlineRepositories
{
    public class AirlineRepository : GenericDAO<Airline>, IAirlineRepository
    {
        public async Task<List<AllAirlinesResponseModel>> GetAllAirlines()
        {
            var list = await Get();
            var result = list.Select(l => new AllAirlinesResponseModel
            {
                Id = l.Id,
                Name = l.Name,
                Status = l.Status,
            });
            return result.ToList();
        }

        public async Task<Airline> GetById(string id)
        {
            return await GetSingle(a => a.Id.Equals(id));
        }

        public async Task<AirlinesResponseModel> GetDetailsById(string id)
        {
            var response = await GetSingle(a => a.Id.Equals(id), includeProperties: "Airplanes");
            var airplanes = response.Airplanes.Select(a => new AirplaneResponseModel
            {
                Id = a.Id,
                AvailableSeat = a.AvailableSeat,
                Code = a.Code,
                Type = a.Type,
            }).ToList();
            return new AirlinesResponseModel
            {
                Id = response.Id,
                Name = response.Name,
                Status = response.Status,
                Airplanes = airplanes
            };
        }
    }
}

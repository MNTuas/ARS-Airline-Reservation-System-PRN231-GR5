using BusinessObjects.Models;
using BusinessObjects.ResponseModels;
using BusinessObjects.ResponseModels.Airlines;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.AirlineRepositories
{
    public class AirlineRepository : GenericDAO<Airline>, IAirlineRepository
    {
        public async Task<List<Airline>> GetAllAirlines()
        {
            var list = await Get();
            return list.ToList();
        }

        public async Task<Airline> GetById(string id)
        {
            return await GetSingle(a => a.Id.Equals(id));
        }
        public async Task<Airline> GetDetailsById(string id)
        {
            return await GetSingle(a => a.Id.Equals(id), includeProperties: "Airplanes.AirplaneSeats.SeatClass");
        }
    }
}

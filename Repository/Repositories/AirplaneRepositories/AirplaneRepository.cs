using BusinessObjects.Models;
using DAO;
using Repository.Repositories.RankRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.AirplaneRepositories
{
    public class AirplaneRepository : GenericDAO<Airplane>, IAirplaneRepository
    {
        public async Task<List<Airplane>> GetAllAirplaneAsync()
        {
            var listAirplane = await Get(includeProperties: "AirplaneSeats");
            return listAirplane.ToList();
        }

        public async Task<List<Airplane>> GetAllActiveAirplanes()
        {
            var listAirplane = await Get(a => a.Status == true, includeProperties: "AirplaneSeats");
            return listAirplane.ToList();
        }

        public async Task<Airplane> GetAirplane(string id)
        {
            var airplane = await GetSingle(r => r.Id.Equals(id), includeProperties: "AirplaneSeats");
            return airplane;
        }
    }
}

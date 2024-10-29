using BusinessObjects.Models;
using DAO;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.RankRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.AirplaneRepositories
{
    public class AirplaneRepository : GenericDAO<Airplane>, IAirplaneRepository
    {
        public async Task<List<Airplane>> GetAllAirplaneAsync()
        {
            var listAirplane = await Get(includeProperties: "AirplaneSeats.SeatClass");
            return listAirplane.ToList();
        }

        public async Task<List<Airplane>> GetAllActiveAirplanes()
        {
            var listAirplane = await Get(a => a.Status == true, includeProperties: "AirplaneSeats.SeatClass,Flights");
            return listAirplane.ToList();
        }

        public async Task<Airplane> GetAirplane(string id)
        {
            var airplane = await GetSingle(
           r => r.Id.Equals(id),
           includeProperties: "AirplaneSeats.SeatClass,Flights"
       );

            return airplane;
        }

        public async Task<Airplane> GetAirplaneByCodeAsync(string airplaneCode)
        {
            var airplane = await GetSingle(r => r.CodeNumber.Equals(airplaneCode));
            return airplane;
        }
    }
            
    }


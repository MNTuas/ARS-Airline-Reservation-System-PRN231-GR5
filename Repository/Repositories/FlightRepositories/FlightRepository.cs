using BusinessObjects.Models;
using BusinessObjects.ResponseModels;
using DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.FlightRepositories
{
    public class FlightRepository : GenericDAO<Flight>, IFlightRepository
    {
        public async Task<Flight> GetById(string id)
        {
            return await GetSingle(a => a.Id.Equals(id));
        }

        public async Task<List<Flight>> GetAllFlights()
        {
            var list = await Get();
            return list.ToList();
        }

        public async Task<List<FlightResponseModel>> GetAllFlightsDetails()
        {
            var list = await Get(includeProperties: "FromNavigation,ToNavigation,Airplane.Airlines");
            return list.Select(x => new FlightResponseModel()
            {
                Id = x.Id,
                Airlines = x.Airplane.Airlines.Name,
                AirlinesId = x.Airplane.Airlines.Id,
                AirplaneCode = x.Airplane.Code,
                AirplaneId = x.AirplaneId,
                DepartureTime = x.DepartureTime,
                ArrivalTime = x.ArrivalTime,
                FromId = x.FromNavigation.Id,
                From = x.FromNavigation.City,
                ToId = x.ToNavigation.Id,
                To = x.ToNavigation.City,
                Status = x.Status
            }).ToList();
        }

        public async Task<Flight> GetFlightById(string id)
        {
            return await GetSingle(f => f.Id.Equals(id), includeProperties: "FromNavigation,ToNavigation,Airplane.Airlines");
        }
    }
}

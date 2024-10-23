using BusinessObjects.Models;
using BusinessObjects.ResponseModels.Flight;
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

        public async Task<List<Flight>> GetAllFlights()
        {
            var list = await Get(includeProperties: "FromNavigation,ToNavigation,Airplane.Airlines");
            return list.ToList();
        }

        public async Task<Flight> GetFlightById(string id)
        {
            return await GetSingle(f => f.Id.Equals(id), includeProperties: "FromNavigation,ToNavigation,Airplane.Airlines,TicketClasses.SeatClass");
        }

        public async Task<List<Flight>> GetFlightsByFilter(string from, string to, DateTime checkin, DateTime? checkout)
        {
            // Lấy tất cả chuyến bay
            var allFlights = await GetAllFlights();

            // Lọc danh sách chuyến bay theo các tiêu chí
            var filteredFlights = allFlights.Where(f => f.From.Equals(from)
                                                      && f.To.Equals(to)
                                                      && f.DepartureTime.Date == checkin.Date);

            // Nếu có ngày check-out, có thể thêm điều kiện lọc cho chuyến bay nếu cần
            if (checkout.HasValue)
            {
                filteredFlights = filteredFlights.Where(f => f.ArrivalTime.Date == checkout.Value.Date);
            }

            return filteredFlights.ToList();
        }

    }
}

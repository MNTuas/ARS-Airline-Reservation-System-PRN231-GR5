using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.FlightRepositories
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        Task<Flight> GetById(string id);    
        Task<List<Flight>> GetAllFlights();
    }
}

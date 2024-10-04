using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.FlightClassRepositories
{
    public interface IFlightClassRepository : IGenericRepository<FlightClass>
    {
        Task<FlightClass> GetById(string id);
        Task<List<FlightClass>> GetAllFlightClasses();
    }
}

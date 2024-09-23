using BusinessObjects.Models;
using Repository.Repositories.AirlineRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.AIrlineServices
{
    public class AirlineService : IAirlineService
    {
        private readonly IAirlineRepository _airlineRepository;

        public AirlineService(IAirlineRepository airlineRepository)
        {
            _airlineRepository = airlineRepository;
        }

        public async Task<List<Airline>> GetAllAirlines()
        {
            return await _airlineRepository.GetAllAirlines();
        }

        public async Task AddAirlines(string name)
        {
            await _airlineRepository.Insert(new Airline
            {
                Id = Guid.NewGuid().ToString(),
                Name = name
            });
        }
    }
}

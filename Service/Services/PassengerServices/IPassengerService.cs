using BusinessObjects.Models;
using BusinessObjects.RequestModels.Passenger;
using FFilms.Application.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.PassengerServices
{
    public interface IPassengerService
    {
        Task<Result<Passenger>> addPassenger(CreatePassengerRequest createPassengerRequest);
    }
}


using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateFlightRequest, Flight>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));
            CreateMap<UpdateFlightRequest, Flight>();
        }
    }
}

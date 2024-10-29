
using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.Airlines;
using BusinessObjects.RequestModels.Airplane;
using BusinessObjects.RequestModels.Airport;
using BusinessObjects.RequestModels.Booking;
using BusinessObjects.RequestModels.Flight;
using BusinessObjects.RequestModels.Passenger;
using BusinessObjects.RequestModels.Ticket;
using BusinessObjects.RequestModels.User;
using BusinessObjects.ResponseModels.Airlines;
using BusinessObjects.ResponseModels.Airplane;
using BusinessObjects.ResponseModels.Airport;
using BusinessObjects.ResponseModels.Flight;
using BusinessObjects.ResponseModels.User;
using Repository.Enums;
using Service.Enums;
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
            //Flight
            CreateMap<CreateFlightRequest, Flight>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => FlightStatusEnums.Schedule.ToString()))
                .ForMember(dest => dest.ArrivalTime, opt => opt.MapFrom(src => src.DepartureTime.AddMinutes(src.Duration)))
                .ForMember(dest => dest.TicketClasses, opt => opt.MapFrom(src => src.TicketClassPrices));
            CreateMap<UpdateFlightRequest, Flight>()
                .ForMember(dest => dest.ArrivalTime, opt => opt.MapFrom(src => src.DepartureTime.AddMinutes(src.Duration)))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Flight, FlightResponseModel>()
                .ForMember(dest => dest.AirlinesId, opt => opt.MapFrom(src => src.Airplane.AirlinesId))
                .ForMember(dest => dest.Airlines, opt => opt.MapFrom(src => src.Airplane.Airlines.Name))
                .ForMember(dest => dest.AirplaneCode, opt => opt.MapFrom(src => src.Airplane.CodeNumber))
                .ForMember(dest => dest.FromName, opt => opt.MapFrom(src => src.FromNavigation.Name))
                .ForMember(dest => dest.ToName, opt => opt.MapFrom(src => src.ToNavigation.Name))
                .ForMember(dest => dest.TicketClassPrices, opt => opt.MapFrom(src => src.TicketClasses));

            //TicketClass
            CreateMap<TicketClassPrice, TicketClass>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => TicketClassStatusEnums.Available.ToString()));
            CreateMap<TicketClassPriceUpdate, TicketClass>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<TicketClass, TicketClassPriceResponse>()
                .ForMember(dest => dest.SeatClassName, opt => opt.MapFrom(src => src.SeatClass.Name));

            //Airlines
            CreateMap<AirlinesCreateModel, Airline>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true));
            CreateMap<Airline, AllAirlinesResponseModel>();
            CreateMap<Airline, AirlinesResponseModel>();

            //Airplane
            CreateMap<AddAirplaneRequest, Airplane>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true));
            CreateMap<UpdateAirplaneRequest, Airplane>();
            CreateMap<Airplane, AirplaneResponseModel>()
                .ForMember(dest => dest.AirplaneSeats, opt => opt.MapFrom(src => src.AirplaneSeats))
                .ForMember(dest => dest.Flights, opt => opt.MapFrom(src => src.Flights));

            //AirplaneSeat
            CreateMap<AirplaneSeatRequest, AirplaneSeat>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));
            CreateMap<AirplaneSeatUpdateRequest, AirplaneSeat>();
            CreateMap<AirplaneSeat, AirplaneSeatResponse>()
                .ForMember(dest => dest.SeatClassName, opt => opt.MapFrom(src => src.SeatClass.Name));

            //Airport
            CreateMap<CreateAirportRequest, Airport>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true));
            CreateMap<Airport, AirportResponseModel>();

            //Booking
            CreateMap<CreateBookingRequest, BookingInformation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => BookingStatusEnums.Pending.ToString()));

            //Passenger
            CreateMap<CreatePassengerRequest, Passenger>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));

            //Ticket
            CreateMap<CreateTicketRequest, Ticket>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => TicketStatusEnums.Pending.ToString()));

            //User
            CreateMap<User, UserInfoResponseModel>()
                .ForMember(dest => dest.RankName, otp => otp.MapFrom(src => src.Rank.Type));
            CreateMap<UserInfoUpdateModel, User>();
        }
    }
}

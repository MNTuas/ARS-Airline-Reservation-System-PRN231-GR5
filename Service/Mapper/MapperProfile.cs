
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
using BusinessObjects.ResponseModels.Booking;
using BusinessObjects.ResponseModels.Flight;
using BusinessObjects.ResponseModels.Passenger;
using BusinessObjects.ResponseModels.Ticket;
using BusinessObjects.ResponseModels.Transaction;
using BusinessObjects.ResponseModels.User;
using Repository.Enums;
using Service.Enums;

namespace Service.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Flight
            CreateMap<CreateFlightRequest, Flight>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => FlightStatusEnums.Scheduled.ToString()))
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
                .ForMember(dest => dest.TicketClassPrices, opt => opt.MapFrom(src => src.TicketClasses))
                .AfterMap((src, dest) =>
                {
                    var airplaneSeats = src.Airplane.AirplaneSeats.ToList();
                    var ticketClasses = src.TicketClasses.ToList();

                    for (int i = 0; i < ticketClasses.Count; i++)
                    {
                        if (i < airplaneSeats.Count)
                        {
                            dest.TicketClassPrices[i].TotalSeat = airplaneSeats[i].SeatCount;
                        }

                        int paidTicketsCount = ticketClasses[i].Tickets
                            .Count(ticket => ticket.Status == BookingStatusEnums.Paid.ToString());
                        dest.TicketClassPrices[i].RemainSeat = airplaneSeats[i].SeatCount - paidTicketsCount;
                    }
                });




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
            CreateMap<UpdateAirportRequest, Airport>();

            //Booking
            CreateMap<CreateBookingRequest, BookingInformation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => BookingStatusEnums.Pending.ToString()));
            CreateMap<BookingInformation, UserBookingResponseModel>()
                .ForMember(dest => dest.Tickets, opt => opt.MapFrom(src => src.Tickets))
                .ForMember(dest => dest.FlightStatus, opt => opt.MapFrom(src => src.Tickets.FirstOrDefault().TicketClass.Flight.Status))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Tickets.FirstOrDefault().TicketClass.Price * src.Quantity))
                ;
            CreateMap<BookingInformation, BookingResponseModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Tickets, opt => opt.MapFrom(src => src.Tickets))
                .ForMember(dest => dest.FlightStatus, opt => opt.MapFrom(src => src.Tickets.FirstOrDefault().TicketClass.Flight.Status))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Tickets.FirstOrDefault().TicketClass.Price * src.Quantity))
                ;

            //Passenger
            CreateMap<CreatePassengerRequest, Passenger>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));
            CreateMap<Passenger, PassengerResposeModel>();
            CreateMap<UpdatePassengerRequest, Passenger>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ReverseMap();
            //Ticket
            CreateMap<CreateTicketRequest, Ticket>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => TicketStatusEnums.Pending.ToString()));
            CreateMap<Ticket, TicketResponseModel>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.TicketClass.SeatClass.Name))
                .ForMember(dest => dest.ClassPrice, opt => opt.MapFrom(src => src.TicketClass.Price));

            //User
            CreateMap<User, UserInfoResponseModel>()
                .ForMember(dest => dest.RankName, otp => otp.MapFrom(src => src.Rank.Type))
                .ForMember(dest => dest.Discount, otp => otp.MapFrom(src => src.Rank.Discount))
                ;
            CreateMap<UserInfoUpdateModel, User>();
            //Transaction
            CreateMap<Transaction, TransactionResponseModel>()
                .ForMember(dest => dest.Booking, otp => otp.MapFrom(src => src.Booking));
            CreateMap<BookingInformation, BookingInformationResponseModel>();
        }
    }
}

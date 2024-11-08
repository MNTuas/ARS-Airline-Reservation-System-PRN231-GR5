using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Services.BookingServices;
using Service.Services.FlightServices;

namespace Service.Services.BackgroundServices
{
    public class EntityUpdateBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityUpdateBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var bookingService = scope.ServiceProvider.GetRequiredService<IBookingService>();
                    var flightService = scope.ServiceProvider.GetRequiredService<IFlightService>();

                    var message1 = await bookingService.AutoUpdateBookingStatus();
                    var message2 = await flightService.AutoUpdateFlightStatus();

                    Console.WriteLine($"Message {message1}");
                    Console.WriteLine($"Message {message2}");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}

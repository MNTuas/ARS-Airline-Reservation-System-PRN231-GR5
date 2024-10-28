using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;
using BusinessObjects.ResponseModels.Flight;
using System.Net.Http.Headers;
using BusinessObjects.RequestModels.Ticket;
using BusinessObjects.RequestModels.Booking;
using BusinessObjects.RequestModels.Airport;
using System.Text.Json;
using Azure;
using FFilms.Application.Shared.Response;

namespace ARS_FE.Pages.UserPage.TicketManagement
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty(SupportsGet = true)]
        public int Quantity { get; set; } // Nhận số lượng vé từ URL

        [BindProperty(SupportsGet = true)]
        public string TicketClassId { get; set; } // Nhận TicketClassId từ URL


        [BindProperty]
        public CreateTicketRequest CreateTicketRequest { get; set; } = default!;

        [BindProperty]
        public List<CreateTicketRequest> Tickets { get; set; } = new List<CreateTicketRequest>();

        public List<Country> Countries { get; set; } = new List<Country>();

        public async Task OnGetAsync(int quantity)
        {
            //lấy quantity để tạo form 
            Quantity = quantity;

            for (int i = 0; i < Quantity; i++)
            {
                Tickets.Add(new CreateTicketRequest
                {
                    TicketClassId = TicketClassId,
                });
            }
            await LoadCountriesAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadCountriesAsync();
                return Page();
            }

            var client = CreateAuthorizedClient();

            var bookingId = await CreateBooking();

            //lặp cái list ticket lấy info 
            foreach (var ticket in Tickets)
            {
                var n = new CreateTicketRequest
                {
                    BookingId = bookingId ,
                    TicketClassId = TicketClassId,
                    Country = ticket.Country,
                    FirstName = ticket.FirstName,
                    LastName = ticket.LastName,
                    Gender = ticket.Gender,
                    Dob = ticket.Dob,
                };

                var response = await APIHelper.PostAsJson(client, "Ticket", n);

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Error occurred while creating the Ticket.");
                    await LoadCountriesAsync();
                    return Page();
                }

            }
            var returnUrlResponse = await APIHelper.PostAsJson(client, $"Transaction", bookingId);
            var returnUrl = await returnUrlResponse.Content.ReadFromJsonAsync<string>();
            return Redirect(returnUrl);
        }

        public async Task<string> CreateBooking()
        {
            var client = CreateAuthorizedClient();

            var bookingRequest = new CreateBookingRequest
            {
                Quantity = Quantity,
            };

            var response = await APIHelper.PostAsJson(client, "Booking", bookingRequest);

            // Check if the response was successful and extract BookingId
            if (response.IsSuccessStatusCode)
            {
                // Đọc nội dung phản hồi JSON bên api
                var responseContent = await response.Content.ReadAsStringAsync();
                var createBookingResponse = JsonDocument.Parse(responseContent);

                // Lấy BookingId từ phản hồi
                string bookingId = createBookingResponse.RootElement.GetProperty("bookingId").GetString();
                return bookingId;
            }

            // Handle error scenario (optional: throw an exception or return null)
            ModelState.AddModelError(string.Empty, "Error occurred while creating the booking.");
            return null;
        }


        private HttpClient CreateAuthorizedClient()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        // Phương thức gọi API lấy danh sách quốc gia
        private async Task LoadCountriesAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://restcountries.com/v3.1/all");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content); // In ra để kiểm tra

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };

                try
                {
                    Countries = JsonSerializer.Deserialize<List<Country>>(content, options);
                    if (Countries != null)
                    {
                        Console.WriteLine($"Total countries: {Countries.Count}");
                    }
                    else
                    {
                        Console.WriteLine("Countries is null.");
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"JSON Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

        public class Country
        {
            public Name Name { get; set; }
        }
        public class Name
        {
            public string Common { get; set; }
        }

    }
}
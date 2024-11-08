using BusinessObjects.RequestModels.Ticket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.UserPage.TicketManagement
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public List<CreateTicketRequest> Tickets { get; set; } = new List<CreateTicketRequest>();

        [BindProperty]
        public CreateTicketRequest CreateTicketRequest { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var client = CreateAuthorizedClient();


            var n = new CreateTicketRequest
            {
                BookingId = CreateTicketRequest.BookingId,
                TicketClassId = CreateTicketRequest.TicketClassId,
                Country = CreateTicketRequest.Country,
                FirstName = CreateTicketRequest.FirstName,
                LastName = CreateTicketRequest.LastName,
                Gender = CreateTicketRequest.Gender,
                Dob = CreateTicketRequest.Dob,
            };

            var response = await APIHelper.PostAsJson(client, "Ticket", n);


            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while creating the Airport.");
                return Page();
            }
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

    }
}

using BusinessObjects.ResponseModels.Airplane;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ARS_FE.Pages.Staff.AirplaneManagement
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public AirplaneResponseModel Airplane { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = CreateAuthorizedClient();

            // Sử dụng APIHelper để gọi API lấy thông tin Airplane
            var response = await APIHelper.GetAsJsonAsync<AirplaneResponseModel>(client, $"airplane/get-airplane/{id}");

            if (response != null)
            {
                Airplane = response;
                return Page();
            }
            else
            {
                return NotFound();
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

using BusinessObjects.ResponseModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.Admin.UserManagement
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public PaginatedList<UserInfoResponseModel> UserInfo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            var client = CreateAuthorizedClient();
            var response = await APIHelper.GetAsJsonAsync<ODataResponse<List<UserInfoResponseModel>>>(client, "users");
            if (response != null)
            {
                UserInfo = PaginatedList<UserInfoResponseModel>.Create(response.Value, pageIndex ?? 1, 6);
                return Page();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> OnPostActivateAsync(string id)
        {
            var client = CreateAuthorizedApiClient();
            var response = await APIHelper.PutAsJson(client, $"users/{id}/status", "Active");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostDeactivateAsync(string id)
        {
            var client = CreateAuthorizedApiClient();
            var response = await APIHelper.PutAsJson(client, $"users/{id}/status", "Inactive");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }

            return BadRequest();
        }

        private HttpClient CreateAuthorizedClient()
        {
            var client = _httpClientFactory.CreateClient("OdataClient");
            var token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        private HttpClient CreateAuthorizedApiClient()
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

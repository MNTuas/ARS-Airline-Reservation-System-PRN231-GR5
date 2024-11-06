using BusinessObjects.RequestModels.User;
using BusinessObjects.ResponseModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.UserPage.Profile
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public UserInfoUpdateModel UserInfo { get; set; } = default!;

        [TempData]
        public string SuccessMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var client = CreateAuthorizedClient();

            var response = await APIHelper.GetAsJsonAsync<UserInfoResponseModel>(client, $"users/own");

            if (response != null)
            {
                UserInfo = new UserInfoUpdateModel
                {
                    Name = response.Name,
                    Email = response.Email,
                    Address = response.Address,
                    PhoneNumber = response.PhoneNumber
                };
                return Page();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = CreateAuthorizedClient();

            var updateModel = UserInfo;

            var response = await APIHelper.PutAsJson(client, $"users/own", updateModel);

            if (response.IsSuccessStatusCode)
            {
                SuccessMessage = "Updated profile successfully";
                return RedirectToPage();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while updating the account info.");
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

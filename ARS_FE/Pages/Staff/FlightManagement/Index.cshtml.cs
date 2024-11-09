using BusinessObjects.ResponseModels.Flight;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ARS_FE.Pages.Staff.FlightManagement
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public PaginatedList<FlightResponseModel> Flight { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public DateTime? FromDate { get; set; }

        [BindProperty]
        public IFormFile UploadedFile { get; set; }


        public async Task<IActionResult> OnGetAsync(int? pageIndex, DateTime? fromDate)
        {
            var client = CreateAuthorizedClient();

            var query = "flights";

            if (fromDate != null)
            {
                FromDate = fromDate;
            }

            if (FromDate.HasValue)
            {
                query += $"?$filter=DepartureTime ge {FromDate.Value.ToString("yyyy-MM-dd")}";
            }

            var response = await APIHelper.GetAsJsonAsync<ODataResponse<List<FlightResponseModel>>>(client, query);
            if (response != null)
            {
                Flight = PaginatedList<FlightResponseModel>.Create(response.Value, pageIndex ?? 1, 5);
                return Page();
            }
            else
            {
                return RedirectToPage("/403Page");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UploadedFile == null || UploadedFile.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Please upload a valid file.");
                return await OnGetAsync(null, null);
            }

            var client = CreateAuthorizedClient();
            using var content = new MultipartFormDataContent();
            using var fileContent = new StreamContent(UploadedFile.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(UploadedFile.ContentType);
            content.Add(fileContent, "file", UploadedFile.FileName);

            var response = await client.PostAsync("https://localhost:7168/api/UploadFlight/UploadExcelFile", content);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                using var jsonDocument = JsonDocument.Parse(responseStream);
                var root = jsonDocument.RootElement;

                bool success = root.GetProperty("success").GetBoolean();
                string message = root.GetProperty("message").GetString();

                if (success)
                {
                    TempData["SuccessMessage"] = message;
                    return RedirectToPage("./Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, message);
                    return await OnGetAsync(null, null);
                }
            }
            else
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                using var jsonDocument = JsonDocument.Parse(responseStream);
                var root = jsonDocument.RootElement;

                string errorMessage = root.TryGetProperty("message", out var messageProperty)
                    ? messageProperty.GetString()
                    : "Error occurred while uploading the file.";

                ModelState.AddModelError(string.Empty, errorMessage);
                return await OnGetAsync(null, null);
            }
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;
using Service.Services.FlightServices;
using Service;
using System.Net.Http.Headers;
using BusinessObjects.ResponseModels.Flight;

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


        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            var client = CreateAuthorizedClient();

            var query = "flights";

            if (FromDate.HasValue)
            {
                query += $"?$filter=DepartureTime ge {FromDate.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")}";
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
                return await OnGetAsync(null); 
            }

            var client = CreateAuthorizedClient();
            using var content = new MultipartFormDataContent();

            using var fileContent = new StreamContent(UploadedFile.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(UploadedFile.ContentType);
            content.Add(fileContent, "file", UploadedFile.FileName);

            var response = await client.PostAsync("https://localhost:7168/api/UploadFlight/UploadExcelFile", content);

            if (response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Upload successfull");
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while uploading the file.");
                return await OnGetAsync(null); // Gọi lại OnGetAsync với null
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

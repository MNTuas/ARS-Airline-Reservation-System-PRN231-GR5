//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Text.Json;

//namespace Service.Services.GetOutsideApi
//{
//    public class CountryService
//    {
//        private readonly HttpClient _httpClient;

//        public CountryService(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        public async Task<List<string>> GetCountriesAsync()
//        {
//            var response = await _httpClient.GetAsync("https://restcountries.com/v3.1/all");

//            if (response.IsSuccessStatusCode)
//            {
//                var content = await response.Content.ReadAsStringAsync();
//                var countries = JsonSerializer.Deserialize<List<Country>>(content);
//                return countries.Select(c => c.Name.Common).ToList();
//            }

//            return new List<string>();
//        }
//    }
//    }

//    public class Country
//    {
//        public Name Name { get; set; }
//    }

//    public class Name
//    {
//        public string Common { get; set; }
//    }
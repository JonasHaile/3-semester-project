using MyBlazorShop.Libraries.Services.Product.Models;
using System.Net.Http.Json;

namespace MyBlazorShop.Web.BlazorWasm.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<ProductModel>> GetProducts()
        {
            return await _httpClient.GetFromJsonAsync<IList<ProductModel>>("https://localhost:7054/v1.0/RentalsApi");
        }
    }
}

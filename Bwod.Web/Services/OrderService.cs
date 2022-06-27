using Bwod.Web.Models;
using Bwod.Web.Services.IServices;
using Bwod.Web.Utils;
using System.Net.Http.Headers;

namespace Bwod.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/order";

        public OrderService(HttpClient client)
        {
            _client = client;   
        }   
        public async Task<List<OrderViewModel>> GetAllOrders(string token)
        {
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client!.GetAsync($"{BasePath}");
            if (response.IsSuccessStatusCode)
                return await response!.ReadContentAs<List<OrderViewModel>>();
            else
                throw new Exception("Something went wrong when calling API");
        }
    }
}

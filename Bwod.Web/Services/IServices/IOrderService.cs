using Bwod.Web.Models;

namespace Bwod.Web.Services.IServices
{
    public interface IOrderService
    {     
        Task<List<OrderViewModel>> GetAllOrders(string token);
    }
}

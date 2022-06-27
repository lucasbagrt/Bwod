using Bwod.OrderAPI.Model;
using Bwod.OrderAPI.Data.ValueObjects;

namespace Bwod.OrderAPI.Repository.IRepository
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderHeader header);
        Task<List<OrderVO>> GetAllOrders();
        Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid);        
    }
}

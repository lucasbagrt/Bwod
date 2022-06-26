using Bwod.OrderAPI.Model;

namespace Bwod.OrderAPI.Repository.IRepository
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderHeader header);
        Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid);        
    }
}

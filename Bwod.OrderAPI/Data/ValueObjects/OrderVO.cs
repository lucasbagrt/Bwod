using Bwod.OrderAPI.Data.ValueObjects;

namespace Bwod.OrderAPI.Data.ValueObjects
{
    public class OrderVO
    {
        public OrderHeaderVO order_header { get; set; }
        public IEnumerable<OrderDetailVO> order_details { get; set; }
    }
}

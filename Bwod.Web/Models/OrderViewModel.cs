namespace Bwod.Web.Models
{
    public class OrderViewModel
    {
        public OrderHeaderViewModel order_header { get; set; }
        public IEnumerable<OrderDetailViewModel> order_details { get; set; }
    }
}

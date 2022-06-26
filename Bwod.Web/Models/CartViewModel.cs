namespace Bwod.Web.Models
{
    public class CartViewModel
    {
        public CartHeaderViewModel cart_header { get; set; }
        public IEnumerable<CartDetailViewModel> cart_details { get; set; }
    }
}

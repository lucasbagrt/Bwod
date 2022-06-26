namespace Bwod.CartAPI.Model
{
    public class Cart
    {
        public CartHeader cart_header { get; set; }
        public IEnumerable<CartDetail> cart_details { get; set; }
    }
}

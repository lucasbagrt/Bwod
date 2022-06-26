namespace Bwod.CartAPI.Data.ValueObjects
{
    public class CartVO
    {
        public CartHeaderVO cart_header { get; set; }
        public IEnumerable<CartDetailVO> cart_details { get; set; }
    }
}

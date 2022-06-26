namespace Bwod.CartAPI.Data.ValueObjects
{
    public class CartDetailVO
    {
        public int id { get; set; }
        public int cart_header_id { get; set; }        
        public CartHeaderVO cart_header { get; set; }
        public int product_id { get; set; }        
        public ProductVO product { get; set; }        
        public int count { get; set; }
    }
}

namespace Bwod.OrderAPI.Data.ValueObjects
{
    public class CartDetailVO
    {
        public int id { get; set; }
        public int cart_header_id { get; set; }                
        public int product_id { get; set; }        
        public virtual ProductVO product { get; set; }        
        public int count { get; set; }
    }
}

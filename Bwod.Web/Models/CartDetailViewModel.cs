namespace Bwod.Web.Models
{
    public class CartDetailViewModel
    {
        public int id { get; set; }
        public int cart_header_id { get; set; }        
        public CartHeaderViewModel cart_header { get; set; }
        public int product_id { get; set; }        
        public ProductViewModel product { get; set; }        
        public int count { get; set; }
    }
}

using Bwod.OrderAPI.Model;

namespace Bwod.OrderAPI.Data.ValueObjects
{
    public class OrderDetailVO
    {
        public int id { get; set; }        
        public int order_header_id { get; set; }        
        public virtual OrderHeader order_header { get; set; }        
        public int product_id { get; set; }
        public int count { get; set; }        
        public string product_name { get; set; }        
        public decimal price { get; set; }
    }
}

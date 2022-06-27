namespace Bwod.Web.Models
{
    public class OrderDetailViewModel
    {
        public int id { get; set; }
        public int order_header_id { get; set; }
        public virtual OrderHeaderViewModel order_header { get; set; }
        public int product_id { get; set; }
        public int count { get; set; }
        public string product_name { get; set; }
        public decimal price { get; set; }
    }
}

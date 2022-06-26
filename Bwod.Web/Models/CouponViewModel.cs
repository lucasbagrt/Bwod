namespace Bwod.Web.Models
{
    public class CouponViewModel
    {
        public int id { get; set; }
        public string coupon_code { get; set; } = "";        
        public decimal discount_amount { get; set; }
    }
}

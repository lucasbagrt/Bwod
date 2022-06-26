namespace Bwod.CouponAPI.Data.ValueObjects
{
    public class CouponVO
    {
        public int id { get; set; }
        public string coupon_code { get; set; } = "";        
        public decimal discount_amount { get; set; }
    }
}

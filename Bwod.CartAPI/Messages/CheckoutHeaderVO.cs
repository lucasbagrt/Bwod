using Bwod.CartAPI.Data.ValueObjects;
using Bwod.MessageBus;

namespace Bwod.CartAPI.Messages
{
    public class CheckoutHeaderVO : BaseMessage
    {        
        public string user_id { get; set; }
        public string coupon_code { get; set; }
        public decimal purchase_amount { get; set; }
        public decimal discount_amount { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime date_time { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string card_number { get; set; }
        public string cvv { get; set; }
        public string expiry_month_year { get; set; }
        public int cart_total_itens { get; set; }
        public IEnumerable<CartDetailVO> cart_details { get; set; }
    }
}

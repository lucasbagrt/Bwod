namespace Bwod.Web.Models
{
    public class OrderHeaderViewModel
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public string coupon_code { get; set; }
        public decimal purchase_amount { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime date_time { get; set; }
        public DateTime order_time { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string card_number { get; set; }
        public string cvv { get; set; }
        public string expiry_month_year { get; set; }
        public int cart_total_itens { get; set; }
        public List<OrderDetailViewModel> order_details { get; set; }
        public bool payment_status { get; set; }

    }
}

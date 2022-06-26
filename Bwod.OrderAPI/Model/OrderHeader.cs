using Bwod.OrderAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bwod.OrderAPI.Model
{
    [Table("order_header")]
    public class OrderHeader : BaseEntity
    {
        [Column("user_id")]
        public string user_id { get; set; }

        [Column("coupon_code")]
        public string coupon_code { get; set; }

        [Column("purchase_amount")]
        public decimal purchase_amount { get; set; }
        
        [Column("first_name")]
        public string first_name { get; set; }

        [Column("last_name")]
        public string last_name { get; set; }

        [Column("purchase_date")]
        public DateTime date_time { get; set; }

        [Column("order_time")]
        public DateTime order_time { get; set; }

        [Column("phone_number")]
        public string phone { get; set; }

        [Column("email")]
        public string email { get; set; }

        [Column("card_number")]
        public string card_number { get; set; }

        [Column("cvv")]
        public string cvv { get; set; }

        [Column("expiry_month_year")]
        public string expiry_month_year { get; set; }

        [Column("total_itens")]
        public int cart_total_itens { get; set; }
        
        public List<OrderDetail> order_details { get; set; }

        [Column("payment_status")]
        public bool payment_status { get; set; }

    }
}

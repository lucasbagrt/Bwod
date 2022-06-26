using Bwod.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bwod.CartAPI.Model
{
    [Table("cart_header")]
    public class CartHeader : BaseEntity
    {
        [Column("user_id")]
        public string user_id { get; set; }
        [Column("coupon_code")]
        public string coupon_code { get; set; }

    }
}

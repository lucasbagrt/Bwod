using Bwod.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bwod.CartAPI.Model
{
    [Table("cart_detail")]
    public class CartDetail : BaseEntity
    {
        public int cart_header_id { get; set; }
        [ForeignKey("cart_header_id")]
        public virtual CartHeader cart_header { get; set; }
        public int product_id { get; set; }
        [ForeignKey("product_id")]
        public virtual Product product { get; set; }
        [Column("count")]
        public int count { get; set; }
    }
}

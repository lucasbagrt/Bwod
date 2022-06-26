using Bwod.OrderAPI.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bwod.OrderAPI.Model
{
    [Table("order_detail")]
    public class OrderDetail : BaseEntity
    {
        public int order_header_id { get; set; }
        [ForeignKey("order_header_id")]
        public virtual OrderHeader order_header { get; set; }

        [Column("product_id")]
        public int product_id { get; set; }

        [Column("count")]
        public int count { get; set; }

        [Column("product_name")]
        public string product_name { get; set; }
        [Column("price")]
        public decimal price { get; set; }
    }
}

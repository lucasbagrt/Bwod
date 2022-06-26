using Bwod.CouponAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bwod.CouponAPI.Model
{
    [Table("Coupon")]
    public class Coupon : BaseEntity
    {                
        [Column("coupon_code")]
        [Required]
        [StringLength(30)]
        public string coupon_code { get; set; } = "";

        [Column("discount_amount")]
        [Required]        
        public decimal discount_amount { get; set; }      
    }
}

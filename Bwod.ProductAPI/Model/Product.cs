using Bwod.ProductAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bwod.ProductAPI.Model
{
    [Table("product")]
    public class Product : BaseEntity
    {
        [Column("name")]
        [Required]
        [StringLength(150)]
        public string name { get; set; } = "";

        [Column("price")]
        [Required]
        [Range(1, 99999999)]        
        public decimal price { get; set; } 

        [Column("description")]
        [StringLength(500)]
        public string? description { get; set; }

        [Column("category_name")]
        [StringLength(50)]
        public string? category_name { get; set; }

        [Column("image_url")]
        [StringLength(300)]
        public string? image_url { get; set; }
    }
}

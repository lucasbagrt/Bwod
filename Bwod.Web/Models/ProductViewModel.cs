using System.ComponentModel.DataAnnotations;

namespace Bwod.Web.Models
{
    public class ProductViewModel
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public decimal price { get; set; }
        public string description { get; set; }
        public string category_name { get; set; }
        public string image_url { get; set; }
        [Range(1, 100)]
        public int Count { get; set; } = 1;
        public string SubstringName()
        {
            if (name.Length < 24)
                return name;
            else
                return $"{name.Substring(0, 21)} ...";
        }
        public string SubstringDescription()
        {
            if (description.Length < 355)
                return description;
            else
                return $"{description.Substring(0, 352)} ...";
        }
    }
}

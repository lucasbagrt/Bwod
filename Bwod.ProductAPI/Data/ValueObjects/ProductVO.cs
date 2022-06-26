namespace Bwod.ProductAPI.Data.ValueObjects
{
    public class ProductVO
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public decimal price { get; set; }        
        public string? description { get; set; }
        public string? category_name { get; set; }
        public string? image_url { get; set; }
    }
}

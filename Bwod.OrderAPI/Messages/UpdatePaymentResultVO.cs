namespace Bwod.OrderAPI.Messages
{
    public class UpdatePaymentResultVO
    {
        public int order_id { get; set; }
        public bool status { get; set; }
        public string email { get; set; }
    }
}

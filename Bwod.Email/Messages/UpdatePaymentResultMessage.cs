namespace Bwod.Email.Messages
{
    public class UpdatePaymentResultMessage
    {
        public int order_id { get; set; }
        public bool status { get; set; }
        public string email { get; set; }
    }
}

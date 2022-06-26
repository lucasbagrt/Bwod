using Bwod.MessageBus;

namespace Bwod.PaymentAPI.Messages
{
    public class UpdatePaymentResultMessage : BaseMessage
    {
        public int order_id { get; set; }
        public bool status { get; set; }
        public string email { get; set; }
    }
}

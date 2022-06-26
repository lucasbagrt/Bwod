﻿using Bwod.MessageBus;

namespace Bwod.OrderAPI.Messages
{
    public class PaymentVO : BaseMessage
    {
        public int order_id { get; set; }
        public string name { get; set; }
        public string card_number { get; set; }
        public string cvv { get; set; }
        public string expiry_month_year { get; set; }
        public decimal purchase_amount { get; set; }
        public string email { get; set; }
    }
}

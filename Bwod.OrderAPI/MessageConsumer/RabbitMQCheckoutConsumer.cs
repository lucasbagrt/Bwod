using Bwod.OrderAPI.Repository;
using Bwod.OrderAPI.Messages;
using Bwod.OrderAPI.Model;
using Bwod.OrderAPI.RabbitMQSender;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Bwod.OrderAPI.MessageConsumer
{
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private readonly OrderRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;
        public RabbitMQCheckoutConsumer(OrderRepository repository, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _repository = repository;
            _rabbitMQMessageSender = rabbitMQMessageSender;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: "checkoutqueue", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                CheckoutHeaderVO vo = JsonSerializer.Deserialize<CheckoutHeaderVO>(content);
                ProcessOrder(vo).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume("checkoutqueue", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessOrder(CheckoutHeaderVO vo)
        {
            OrderHeader order = new()
            {
                user_id = vo.user_id,
                card_number = vo.card_number,
                cart_total_itens = vo.cart_total_itens,
                coupon_code = vo.coupon_code,
                cvv = vo.cvv,
                date_time = vo.date_time,
                email = vo.email,
                expiry_month_year = vo.expiry_month_year,
                first_name = vo.first_name,
                order_time = DateTime.Now,
                payment_status = false,
                last_name = vo.last_name,
                order_details = new List<OrderDetail>(),
                phone = vo.phone,
                purchase_amount = vo.purchase_amount
            };

            foreach (var details in vo.cart_details)
            {
                OrderDetail detail = new()
                {
                    price = details.product.price,
                    product_id = details.product_id,
                    product_name = details.product.name,
                    count = details.count
                };
                order.cart_total_itens += details.count;
                order.order_details.Add(detail);
            }
            await _repository.AddOrder(order);

            PaymentVO payment = new()
            {
                name = order.first_name + " " + order.last_name,
                card_number = order.card_number,
                cvv = order.cvv,
                email = order.email,
                expiry_month_year = order.expiry_month_year,
                order_id = order.id,
                purchase_amount = order.purchase_amount
            };
            try
            {
                _rabbitMQMessageSender.SendMessage(payment, "orderpaymentprocessqueue");
            }
            catch (Exception)
            {
                //log ex 
                throw;
            }
        }
    }
}

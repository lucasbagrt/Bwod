using Bwod.OrderAPI.Model;
using Bwod.OrderAPI.Model.Context;
using Bwod.OrderAPI.Repository.IRepository;
using Bwod.OrderAPI.Data.ValueObjects;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Bwod.OrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<MySQLContext> _context;
        private IMapper _mapper;

        public OrderRepository(DbContextOptions<MySQLContext> context)
        {
            _context = context;

        }

        public async Task<bool> AddOrder(OrderHeader header)
        {
            if (header == null) return false;
            await using var _db = new MySQLContext(_context);
            _db.Headers.Add(header);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<OrderVO>> GetAllOrders()
        {
            try
            {
                await using var _db = new MySQLContext(_context);
                var orders = (from a in _db.Headers                        
                              select new OrderVO
                              {
                                  order_header = new OrderHeaderVO
                                  {
                                      card_number = a.card_number,
                                      cart_total_itens = a.cart_total_itens,
                                      coupon_code = a.coupon_code,  
                                      cvv = a.cvv,
                                      date_time = a.date_time,
                                      email = a.email,
                                      expiry_month_year = a.expiry_month_year,
                                      first_name = a.first_name,
                                      id = a.id,
                                      last_name = a.last_name,
                                      order_time = a.order_time,
                                      payment_status = a.payment_status,
                                      phone = a.phone,
                                      purchase_amount = a.purchase_amount,
                                      user_id = a.user_id                                      
                                  },
                                  order_details = new List<OrderDetailVO>()
                              }).OrderByDescending(x => x.order_header.date_time).ToList();                
                foreach (var item in orders)
                {
                    item.order_details = _db.Details.Where(t => t.order_header_id == item.order_header.id).ToList().Select(x => new OrderDetailVO
                    {
                        id = x.id,
                        count = x.count,
                        order_header_id = x.order_header_id,
                        price = x.price,
                        product_id = x.product_id,
                        product_name = x.product_name
                    });
                }
                return orders;
            }
            catch (Exception)
            {

                return new List<OrderVO>();
            }
        }

        public async Task UpdateOrderPaymentStatus(int orderHeaderId, bool status)
        {
            await using var _db = new MySQLContext(_context);
            var header = await _db.Headers.FirstOrDefaultAsync(t => t.id == orderHeaderId);
            if (header != null)
            {
                header.payment_status = status;
                await _db.SaveChangesAsync();
            }
        }
    }
}

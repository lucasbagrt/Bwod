using Bwod.OrderAPI.Model;
using Bwod.OrderAPI.Model.Context;
using Bwod.OrderAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bwod.OrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<MySQLContext> _context;

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

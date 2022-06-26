using Bwod.Email.Messages;
using Bwod.Email.Model;
using Bwod.Email.Model.Context;
using Bwod.Email.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bwod.Email.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<MySQLContext> _context;

        public EmailRepository(DbContextOptions<MySQLContext> context)
        {
            _context = context;
        }
        public async Task LogEmail(UpdatePaymentResultMessage message)
        {
            //SEND EMAIL
            EmailLog email = new()
            {
                email = message.email,
                send_date = DateTime.Now,
                log = $"Order - {message.order_id} has been created successfully!"
            };
            await using var _db = new MySQLContext(_context);
            _db.Emails.Add(email);
            await _db.SaveChangesAsync();
        }
    }
}

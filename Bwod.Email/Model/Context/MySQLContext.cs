using Bwod.Email.Model;
using Microsoft.EntityFrameworkCore;

namespace Bwod.Email.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() {}
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}
        public DbSet<EmailLog> Emails { get; set; }        
    }
}

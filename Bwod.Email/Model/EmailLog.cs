using Bwod.Email.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bwod.Email.Model
{
    [Table("email_logs")]
    public class EmailLog : BaseEntity
    {
        [Column("email")]
        public string email { get; set; }

        [Column("log")]
        public string log { get; set; }
        [Column("sent_date")]
        public DateTime send_date { get; set; }
    }
}

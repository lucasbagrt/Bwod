using Bwod.Email.Messages;
using Bwod.Email.Model;

namespace Bwod.Email.Repository.IRepository
{
    public interface IEmailRepository
    {        
        Task LogEmail(UpdatePaymentResultMessage message);        
    }
}

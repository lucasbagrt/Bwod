using Bwod.Web.Models;

namespace Bwod.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<CouponViewModel> GetCoupon(string code, string token);      
    }
}

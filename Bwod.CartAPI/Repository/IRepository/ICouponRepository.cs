using Bwod.CartAPI.Data.ValueObjects;

namespace Bwod.CartAPI.Repository.IRepository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCoupon(string couponCode, string token);
    }
}

using Bwod.CouponAPI.Data.ValueObjects;

namespace Bwod.CouponAPI.Repository.IRepository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCouponCode(string couponCode);
    }
}

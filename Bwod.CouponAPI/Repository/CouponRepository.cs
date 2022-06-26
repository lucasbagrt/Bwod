using AutoMapper;
using Bwod.CouponAPI.Data.ValueObjects;
using Bwod.CouponAPI.Model.Context;
using Bwod.CouponAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bwod.CouponAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public CouponRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CouponVO> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(t => t.coupon_code == couponCode);
            return _mapper.Map<CouponVO>(coupon);
        }
    }
}

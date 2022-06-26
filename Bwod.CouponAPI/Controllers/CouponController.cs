using Bwod.CouponAPI.Data.ValueObjects;
using Bwod.CouponAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Bwod.CouponAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private ICouponRepository _repository;
        public CouponController(ICouponRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }
       
        [HttpGet("{couponCode}")]        
        public async Task<ActionResult<CouponVO>> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _repository!.GetCouponByCouponCode(couponCode);
            if (coupon == null) return NotFound();
            return Ok(coupon);
        }       
    }
}

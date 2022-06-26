using Bwod.Web.Models;
using Bwod.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bwod.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;
        public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await FindUserCart());
        }
        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(t => t.Type == "sub")?.FirstOrDefault()?.Value;
            var response = await _cartService.ApplyCoupon(model, token);
            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }
        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon(CartViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.RemoveCoupon(userId, token);

            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }
        public async Task<IActionResult> Remove(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(t => t.Type == "sub")?.FirstOrDefault()?.Value;
            var response = await _cartService.RemoveFromCart(id, token);

            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            return View(await FindUserCart());
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CartViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");            
            var response = await _cartService.Checkout(model.cart_header, token);

            if (response != null && response.GetType() == typeof(string))
            {
                TempData["Error"] = response;
                return RedirectToAction(nameof(Checkout));
            }

            else if (response != null)
            {
                return RedirectToAction(nameof(Confirmation));
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Confirmation()
        {
            return View();
        }
        private async Task<CartViewModel> FindUserCart()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(t => t.Type == "sub")?.FirstOrDefault()?.Value;
            var response = await _cartService.FindCartByUserId(userId, token);
            if (response?.cart_header != null)
            {
                if (!string.IsNullOrEmpty(response.cart_header.coupon_code))
                {
                    var coupon = await _couponService.GetCoupon(response.cart_header.coupon_code, token);

                    if (coupon?.coupon_code != null)
                    {
                        response.cart_header.discount_amount = coupon.discount_amount;
                    }
                }
                foreach (var detail in response.cart_details)
                {
                    response.cart_header.purchase_amount += (detail.product.price * detail.count);
                }
                response.cart_header.purchase_amount -= response.cart_header.discount_amount;                
            }
            return response;
        }
    }
}

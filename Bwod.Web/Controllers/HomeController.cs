using Bwod.Web.Models;
using Bwod.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bwod.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }
        public async Task<IActionResult> Index()
        {            
            var products = await _productService!.FindAllProducts("");
            return View(products);
        }
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var model = await _productService!.FindProductById(id, token);
            return View(model);
        }
        [HttpPost]
        [ActionName("Details")]
        [Authorize]
        public async Task<IActionResult> DetailsPost(ProductViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            CartViewModel cart = new()
            {
                cart_header = new CartHeaderViewModel
                {
                    user_id = User.Claims.Where(t => t.Type == "sub")?.FirstOrDefault()?.Value
                }
            };
            CartDetailViewModel cartDetail = new()
            {
                count = model.Count,
                product_id = model.id,
                product = await _productService.FindProductById(model.id, token),
            };
            var cartDetails = new List<CartDetailViewModel>();
            cartDetails.Add(cartDetail);
            cart.cart_details = cartDetails;

            var response = await _cartService.AddItemToCart(cart, token);
            if (response != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
        [Authorize]
        public async Task<IActionResult> Login()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
        }
    }
}
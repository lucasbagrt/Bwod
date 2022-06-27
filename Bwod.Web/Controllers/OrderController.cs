using Bwod.Web.Models;
using Bwod.Web.Services.IServices;
using Bwod.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bwod.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> OrderIndex()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _orderService!.GetAllOrders(token);
            return View(response);
        }
    }
}

using Bwod.OrderAPI.Data.ValueObjects;
using Bwod.OrderAPI.Repository.IRepository;
using Bwod.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Bwod.OrderAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository _repository;
        public OrderController(IOrderRepository repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }       
        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<List<OrderVO>>> GetAllOrders()
        {
            var result = await _repository!.GetAllOrders();
            if (result == null) return BadRequest();
            return Ok(result);
        }
    }
}

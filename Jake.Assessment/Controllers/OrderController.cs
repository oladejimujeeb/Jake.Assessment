using Jake.Assessment.DTO;
using Jake.Assessment.Implementation.Services;
using Jake.Assessment.Interface.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jake.Assessment.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseModel), 200)]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest model) 
        {
            if (model == null)
            {
                return BadRequest("Invalid Model");
            }
            var createOrder = await _orderService.CreateOrder(model);
            if (!createOrder.Status)
            {
                return BadRequest();
            }
            return Ok(createOrder);
        }
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponseModel<IEnumerable<AllOrderViewModel>>), 200)]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            if(!orders.Status) 
            {
                BadRequest(orders);
            }
            return Ok(orders);
        }
    }
}

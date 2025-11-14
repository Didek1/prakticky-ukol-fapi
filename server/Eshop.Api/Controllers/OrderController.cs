using Eshop.Api.Interfaces;
using Eshop.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager orderManager;

        public OrderController(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        [HttpGet("order/{id}")]
        public IActionResult GetOrder(uint id)
        {
            OrderDto? order = orderManager.GetOrder(id);

            if(order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost("order")]
        public IActionResult AddOrder([FromBody] OrderDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrderDto? createdOrder = orderManager.AddOrder(order);

            // Vraci status 201 Created s novou objednavkou
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        }
    }
}

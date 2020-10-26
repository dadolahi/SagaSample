using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Abstraction.DbContexts;
using OrderService.Endpoints.Api.Services;

namespace OrderService.Endpoints.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartConsumersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromServices]  InventoryOrderAcceptedConsumer inventoryOrderAcceptedConsumer,
                                 [FromServices]  InventoryOrderRejectedConsumer inventoryOrderRejectedConsumer)
        { 
            inventoryOrderAcceptedConsumer.Consume();
            inventoryOrderRejectedConsumer.Consume();
            return Ok("Consumer Started");
        }
    }
}
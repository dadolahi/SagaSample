using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderService.Core.Abstraction.Services;
using OrderService.Core.Domain.Entities;
using OrderService.Endpoints.Api.Model.Orders;
using OrderService.Infra.Services;
using SagaSample.Share.Common.Abstract;
using System.Linq;

namespace OrderService.Endpoints.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private IOrderService _orderService;
        public OrderController(ILogger<OrderController> logger, IOrderService orderSeRvice)
        {
            _logger = logger;
            _orderService = orderSeRvice;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var orders = _orderService.GetAll().OrderByDescending(x => x.Id);
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var order = _orderService.GetById(id);
            return Ok(order);
        }
        [HttpPost]
        public IActionResult Create(CreateVM model, [FromServices] OrderCreateCommandHandler orderCreateCommandHandler)
        {
            var order = new OrderCreateCommand
            {
                CustomerName = model.CustomerName,
                ProductIds = model.ProductIds
            };
            orderCreateCommandHandler.Execute(order);
            return Ok(order);
        }
    }
}
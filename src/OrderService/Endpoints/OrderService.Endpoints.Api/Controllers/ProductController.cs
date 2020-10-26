using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderService.Core.Abstraction.Services;

namespace OrderService.Endpoints.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private IProductService _productService;

        public ProductController(ILogger<OrderController> logger, IProductService productSeRvice)
        {
            _logger = logger;
            _productService = productSeRvice;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var orders = _productService.GetAll();
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var orders = _productService.GetById(id);
            return Ok(orders);
        }
        
    }
}
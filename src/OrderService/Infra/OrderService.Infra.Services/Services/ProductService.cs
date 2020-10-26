using OrderService.Core.Abstraction.DbContexts;
using OrderService.Core.Abstraction.Services;
using OrderService.Core.Domin.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.Infra.Services.Services
{
    public class ProductService : IProductService
    {
        IOrderServicedbContext _context;

        public ProductService(IOrderServicedbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll() => _context.Products.ToList();
        public Product GetById(int id) => _context.Products.Find(id);
    }
}

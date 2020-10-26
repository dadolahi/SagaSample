using OrderService.Core.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Core.Abstraction.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int Id);
    }
}

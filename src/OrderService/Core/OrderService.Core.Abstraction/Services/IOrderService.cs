using OrderService.Core.Domin.Entities;
using System.Collections.Generic;

namespace OrderService.Core.Abstraction.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        Order GetById(int id);
    }
}

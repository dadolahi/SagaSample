using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Core.Abstraction.DbContexts;
using OrderService.Core.Abstraction.Services;
using OrderService.Core.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using OrderService.Infra.DataLayer.Contexts;

namespace OrderService.Infra.Services.Services
{
    public class OrderService : IOrderService
    {
         IOrderServicedbContext _context;
        public OrderService(IOrderServicedbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll() => _context.Orders.ToList();
        public Order GetById(int id) => _context.Orders.Include(x => x.Products).FirstOrDefault(x => x.Id == id);
    }
}

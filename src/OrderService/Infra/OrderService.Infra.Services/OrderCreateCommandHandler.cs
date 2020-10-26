using OrderService.Core.Abstraction.DbContexts;
using OrderService.Core.Domain.Entities;
using OrderService.Core.Domin.Entities;
using SagaSample.Share.Common.Abstract;
using SagaSample.Share.Messages;
using System;
using System.Linq;

namespace OrderService.Infra.Services
{
    public class OrderCreateCommandHandler
    {
        private readonly IOrderServicedbContext _context;
        private readonly IRabbitMQPublisher _rabbitMQPublisher;

        public OrderCreateCommandHandler(IOrderServicedbContext context, IRabbitMQPublisher rabbitMQPublisher)
        {
            _context = context;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public Order Execute(OrderCreateCommand command)
        {
            var product = _context.Products.Where(prodcut => command.ProductIds.Contains(prodcut.Id)).ToList();


            var CorrelationId = Guid.NewGuid().ToString();
            var order = new Order
            {
                CorrelationId = CorrelationId,
                CustomerName = command.CustomerName,
                Products = product.Select(p=> new OrderProduct {ProdcutId=p.Id }).ToList(),
                State = "Pending"
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
            var orderMessage = new OrderMessage
            {
                CorrelationId = CorrelationId,
                CustomerName = command.CustomerName,
                State = order.State,
                ProductIds = command.ProductIds
            };
            _rabbitMQPublisher.Publish<OrderMessage>(orderMessage, "CreateOrder", "fanout", "");
            return order;
        }
    }
}

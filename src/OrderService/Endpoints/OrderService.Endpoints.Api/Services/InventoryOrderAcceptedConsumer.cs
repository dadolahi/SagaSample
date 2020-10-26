using Microsoft.EntityFrameworkCore;
using OrderService.Core.Domin.Entities;
using OrderService.Infra.DataLayer.Contexts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SagaSample.Share.Messages;
using SagaSample.Share.Toolkit;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Endpoints.Api.Services
{
    public class InventoryOrderAcceptedConsumer
    {
        public static IConnectionFactory _connectionFactory;
        public static IConnection _connection;
        public static IModel _model;
        public static string QueueName;
        public const string ExchangeName = "InventoryOrderAccepted";

        public InventoryOrderAcceptedConsumer()
        {
            CreateConnetion();
        }
        public async Task Consume()
        {
            var consumer = new EventingBasicConsumer(_model);

            consumer.Received += async(_model, ea) =>
           {
               var body = ea.Body.ToArray();
               var message = body.FromByteArray<OrderMessage>();
               ConsumAcceptedOrder(message.CorrelationId, "Accepted");
           };
            _model.BasicConsume(queue: QueueName, true, consumer);
        }
        public static void CreateConnetion()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = Protocols.DefaultProtocol.DefaultPort

            };
            _connection = _connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            QueueName = _model.QueueDeclare(queue: "InventoryOrderAcceptedQueue");
            _model.QueueBind(QueueName, ExchangeName, "");
        }
        private Order ConsumAcceptedOrder(string id, string state)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderServiceDbContext>();
            optionsBuilder.UseSqlServer("Server =.\\SQL2016; Database = TestDB; Trusted_Connection = True; MultipleActiveResultSets = true");

            var dataContext = new OrderServiceDbContext(optionsBuilder.Options);

            var order = dataContext.Orders.Include(x => x.Products).FirstOrDefault(x => x.CorrelationId == id);
            if (order != null)
            {
                order.State = state;
                dataContext.SaveChanges();
            }
            return order;
        }

    }
}

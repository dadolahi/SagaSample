using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SagaSample.Share.Messages;
using SagaSample.Share.Toolkit;
using System;
using System.Linq;

namespace InventoryService.Endpoints.App
{

    class Program
    {
        public static IConnectionFactory _connectionFactory;
        public static IConnection _connection;
        public static IModel _model;
        public static string QueueName;
        public const string ExchangeName = "CreateOrder";
        public static int MessageCounter { get; private set; }

        static void Main(string[] args)
        {
            var option = new RabbitOptions
            {
                UserName = "guest",
                Password = "guest",
                HostName = "localhost",
                VHost = "/",
                Port = 5672
            };
            CreateConnetion();
            var consumer = new EventingBasicConsumer(_model);
            var publisher = new RabbitMQPublisher(option);


            consumer.Received += (_model, ea) =>
           {
               var body = ea.Body.ToArray();
               var order = body.FromByteArray<OrderMessage>();
               MessageCounter++;
               if (order.ProductIds.Any(productId => productId > 4))
               {
                   // do anything;
                   order.State = "Reject";
                   publisher.Publish<OrderMessage>(order, "InventoryOrderRejected", ExchangeType.Fanout);
                   Console.WriteLine("Order Rejected By Inventory System");
               }
               else
               {
                   // do anything;
                   order.State = "Accept";
                   publisher.Publish<OrderMessage>(order, "InventoryOrderAccepted", ExchangeType.Fanout);
                   Console.WriteLine("Order Accepted By Inventory System");
               }

           };
            _model.BasicConsume(queue: QueueName, true, consumer);
            Console.WriteLine($"");
            Console.ReadLine();
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
            QueueName = _model.QueueDeclare("InventoryReciveMessage").QueueName;
            _model.QueueBind(QueueName, ExchangeName, "");
        }
    }
}

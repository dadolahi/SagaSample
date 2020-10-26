using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SagaSample.Share.Messages;
using SagaSample.Share.Toolkit;
using System;
using System.Linq;

namespace DeliverService.Endpoints.App
{
    class Program
    {
        public static IConnectionFactory _connectionFactory;
        public static IConnection _connection;
        public static IModel _model;
        public static string QueueName;
        public const string ExchangeName = "InventoryOrderAccepted";
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

            consumer.Received += (_model, ea) =>
            {
                var body = ea.Body.ToArray();
                var order = body.FromByteArray<OrderMessage>();

                Console.WriteLine($"A Order with CoorrelationId: {order.CorrelationId}, CustomerNumber: {order.CustomerName}, ProductIds: {String.Join(',', order.ProductIds)} Recived. ");

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
            QueueName = _model.QueueDeclare("OrderDeliverySystem").QueueName;
            _model.QueueBind(QueueName, ExchangeName, "");
        }
    }
}
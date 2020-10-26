//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Options;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using SagaSample.Share.Common.Abstract;
//using SagaSample.Share.Toolkit;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace SagaSample.Share.RabbitPublisher
//{
//    public class RabbitMQSubscriber : BackgroundService, IRabbitMQSubscriber
//    {
       
//        public static IConnectionFactory _connectionFactory;
//        public static IConnection _connection;
//        private IModel _channel;
//        public static string QueueName;
//        public const string ExchangeName = "PubSub_Exchange";
//        private readonly RabbitOptions _options;

//        public RabbitMQSubscriber(IOptions<RabbitOptions> optionsAccs)
//        {
//            _options = optionsAccs.Value;
//            CreateConnetion();
//        }

//        private void CreateConnetion()
//        {

//            _connectionFactory = new ConnectionFactory
//            {
//                HostName = "localhost",
//                UserName = "guest",
//                Password = "guest",
//                Port = Protocols.DefaultProtocol.DefaultPort
//            };
//            _connection = _connectionFactory.CreateConnection();
//            _channel = _connection.CreateModel();
//        }


//        protected override Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            stoppingToken.ThrowIfCancellationRequested();
//            var queueName = _channel.QueueDeclare().QueueName;
//            _channel.QueueBind(queueName, ExchangeName, "");
//            var consumer = new EventingBasicConsumer(_channel);
//            consumer.Received += (ch, ea) =>
//            {
//                // received message  
//                var content = System.Text.Encoding.UTF8.GetString(ea.Body);

//                // handle the received message  
//                HandleMessage(content);
//                _channel.BasicAck(ea.DeliveryTag, false);
//            };

//            consumer.Shutdown += OnConsumerShutdown;
//            consumer.Registered += OnConsumerRegistered;
//            consumer.Unregistered += OnConsumerUnregistered;
//            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

//            _channel.BasicConsume("demo.queue.log", false, consumer);
//            return Task.CompletedTask;
//        }

//        public bool Lissene<TMessage>(TMessage message, string queueName)
//        {
//            throw new NotImplementedException();
//        }

//        _channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

//            var consumer = new EventingBasicConsumer(_channel);
//            consumer.Received += (model, ea) =>
//            {
//                var body = ea.Body;
//                var message = Encoding.UTF8.GetString(body);
//                int m = 0;
//            };
//            channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);




//            _connectionFactory = new ConnectionFactory
//            {
//                HostName = _options.HostName,
//                UserName = _options.UserName,
//                Password = _options.Password,
//                Port = _options.Port,
//                VirtualHost = _options.VHost,
//            };
//            _connection = _connectionFactory.CreateConnection();
//        }


//        private void HandleMessage(string content)
//        {
//            // we just print this message   
//            _logger.LogInformation($"consumer received {content}");
//        }

//        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
//        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
//        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
//        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
//        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

//        public override void Dispose()
//        {
//            _channel.Close();
//            _connection.Close();
//            base.Dispose();
//        }

        
//    }
//}

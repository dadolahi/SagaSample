using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SagaSample.Share.Common.Abstract;
using SagaSample.Share.Toolkit;
using System;

namespace SagaSample.Share.RabbitPublisher
{
    public class RabbitMQPublisher : IRabbitMQPublisher
    {
        public static IConnectionFactory _connectionFactory;
        public static IConnection _connection;
        public static IModel _model;
        private readonly RabbitOptions _options;
        public RabbitMQPublisher(IOptions<RabbitOptions> optionsAccs)
        {
            _options = optionsAccs.Value;
            CreateConnetion();
        }

        public bool Publish<TMessage>(TMessage message, string exchangeName, string exchangeType = "direct", string routeKey = "") where TMessage : class
        {
            if (message == null)
                return false;

            //var channel = _objectPool.Get();

            try
            {
                _model = _connection.CreateModel();
                _model.ExchangeDeclare(exchangeName, exchangeType, true, false);

                _model.BasicPublish(exchangeName, exchangeName, null, message.ToByteArray());

                return true;
                //channel.ExchangeDeclare(exchangeName, exchangeType, true, false, null);

                //var sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                //var properties = channel.CreateBasicProperties();
                //properties.Persistent = true;

                //channel.BasicPublish(exchangeName, routeKey, properties, sendBytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        //private IConnection GetConnection()
        //{
        //    var factory = new ConnectionFactory()
        //    {
        //        HostName = _options.HostName,
        //        UserName = _options.UserName,
        //        Password = _options.Password,
        //        Port = _options.Port,
        //        VirtualHost = _options.VHost,
        //    };

        //    return factory.CreateConnection();
        //}
        private void CreateConnetion()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = _options.HostName,
                UserName = _options.UserName,
                Password = _options.Password,
                Port = _options.Port,
                VirtualHost = _options.VHost,
            };
            _connection = _connectionFactory.CreateConnection();
            //  _model = _connection.CreateModel();
            //  _model.ExchangeDeclare(ExchangeName, ExchangeType.Fanout, false);
        }
    }
}

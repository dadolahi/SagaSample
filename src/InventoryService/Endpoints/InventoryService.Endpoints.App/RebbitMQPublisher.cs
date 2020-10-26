using RabbitMQ.Client;
using SagaSample.Share.Toolkit;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Endpoints.App
{
    public class RabbitMQPublisher 
    {
        public static IConnectionFactory _connectionFactory;
        public static IConnection _connection;
        public static IModel _model;
        private readonly RabbitOptions _options;
        public RabbitMQPublisher(RabbitOptions optionsAccs)
        {
            _options = optionsAccs;
            CreateConnetion();
        }

        public bool Publish<TMessage>(TMessage message, string exchangeName, string exchangeType = "fanout", string routeKey = "") where TMessage : class
        {
            if (message == null)
                return false;
            try
            {
                _model = _connection.CreateModel();
                _model.ExchangeDeclare(exchangeName, exchangeType, true, false);

                _model.BasicPublish(exchangeName, exchangeName, null, message.ToByteArray());

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
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
          
        }
    }
}

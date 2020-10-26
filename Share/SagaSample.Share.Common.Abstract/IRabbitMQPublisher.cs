using System;
using System.Collections.Generic;
using System.Text;

namespace SagaSample.Share.Common.Abstract
{
    public interface IRabbitMQPublisher
    {
         bool Publish<TMessage>(TMessage message, string exchangeName, string exchangeType = "direct", string routeKey = "") where TMessage: class;
    }
}

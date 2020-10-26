namespace SagaSample.Share.Common.Abstract
{
    public interface IRabbitMQSubscriber
    {
        bool Lissene<TMessage>(TMessage message, string queueName);
    }
}

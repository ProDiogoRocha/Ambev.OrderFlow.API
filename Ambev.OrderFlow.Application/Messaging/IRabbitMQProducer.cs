using Ambev.OrderFlow.Application.Messages;

namespace Ambev.OrderFlow.Application.Messaging
{
    public interface IRabbitMQProducer : IMessageBus, IDisposable
    {
    }
}

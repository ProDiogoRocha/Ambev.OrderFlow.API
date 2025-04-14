namespace Ambev.OrderFlow.Application.Messaging
{
    public interface IMessageBus
    {
        Task PublicarAsync<T>(T message);
    }
}

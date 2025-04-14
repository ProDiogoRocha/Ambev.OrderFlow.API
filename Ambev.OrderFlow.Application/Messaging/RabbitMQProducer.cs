using Ambev.OrderFlow.Infrastructure.Data.Configurations;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Ambev.OrderFlow.Application.Messaging
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly IConnection _connection;
        private readonly RabbitMqSettings _settings;
        private readonly IChannel _channel;

        public RabbitMQProducer(IOptions<RabbitMqSettings> options)
        {
            _settings = options.Value;
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }

        public async Task PublicarAsync<PedidoAmbevMessage>(PedidoAmbevMessage message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _settings.Host,
                UserName = _settings.Username,
                Password = _settings.Password
            };

            await using var _connection = await factory.CreateConnectionAsync();
            await using var _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(queue: "pedidos-ambev",
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            var properties = new BasicProperties
            {
                DeliveryMode = DeliveryModes.Persistent
            };

            await _channel.BasicPublishAsync(
                exchange: "",
                routingKey: "pedidos-ambev",
                mandatory: false,
                basicProperties: properties,
                body: body);
        }
    }
}

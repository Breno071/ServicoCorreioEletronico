using RabbitMQ.Client;
using System.Text.Json;

namespace Producer
{
    public class Producer : IProducer
    {
        private const string QUEUE_NAME = "Adoptions";
        private readonly ConnectionFactory _factory;

        public Producer(ConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task Send(object message)
        {
            await Task.Run(() =>
            {
                using var connection = _factory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(
                    QUEUE_NAME,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                var stringfiedMessage = JsonSerializer.Serialize(message);
                var bytesMessage = System.Text.Encoding.UTF8.GetBytes(stringfiedMessage);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: QUEUE_NAME,
                    basicProperties: null,
                    body: bytesMessage
                    );

                return Task.CompletedTask;
            });
        }
    }
}

using API.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace API.Consumer
{
    public class Consumer :IConsumer
    {
        private readonly ConnectionFactory _factory;
        private const string QUEUE_NAME = "Mensagens";
        public Consumer(ConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task Consume()
        {
            List<Task> tasks =
            [
                Task.Run(() =>
                {
                    using var connection = _factory.CreateConnection();
                    using var channel = connection.CreateModel();

                    channel.QueueDeclare(queue: QUEUE_NAME,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                    };
                    channel.BasicConsume(queue: QUEUE_NAME,
                                         autoAck: true,
                                         consumer: consumer);
                })
            ];

            await Task.WhenAll(tasks);
        }
    }
}

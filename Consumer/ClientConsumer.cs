using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public class ClientConsumer
    {
        private readonly ConnectionFactory _factory = new()
        {
            HostName = "localhost"
        };
        private const string QUEUE_NAME = "Adoptions";

        public async Task Consume()
        {
            List<Task> tasks =
               [
                   #region Busca as mensagens da fila Adoptions
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
                   #endregion
               ];

            await Task.WhenAll(tasks);
        }
    }
}

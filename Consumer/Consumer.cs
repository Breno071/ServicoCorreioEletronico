using AppRepository.Data;
using AppRepository.Entities;
using AppRepository.Interfaces;
using AppRepository.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utills.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ConsumerWindowsService
{
    public class Consumer
    {
        private readonly ConnectionFactory _factory = new()
        {
            HostName = "localhost"
        };
        private const string QUEUE_NAME = "Adoptions";
        private readonly ApplicationContext _context;
        private IModel channel;
        private IConnection _connection;

        public Consumer(ApplicationContext context)
        {
            _context = context;
            _connection = _factory.CreateConnection();
            channel = _connection.CreateModel();
        }

        public async Task<List<AdoptRequest>> GetMessages()
        {
            List<AdoptRequest> messages = [];

            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: QUEUE_NAME,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

            var tcs = new TaskCompletionSource<object>();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.Out.WriteLine(body.ToString());
                Console.Out.WriteLine(message);
                try
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        AdoptRequest request = JsonSerializer.Deserialize<AdoptRequest>(message);

                        if (request is not null)
                            messages.Add(request);
                        else
                            throw new Exception($"Não foi possível deserializar a mensagem {message}");
                    }

                    //Informa que a mensagem foi consumida
                    tcs.SetResult(true);
                }
                catch (JsonException jsonEx)
                {
                    ILogRepository logRepository = new LogRepository(_context);
                    logRepository.Add(new Log()
                    {
                        LogType = LogType.Error,
                        Message = jsonEx.Message
                    });
                }
                catch (Exception)
                {

                    throw;
                }
            };

            channel.BasicConsume(queue: QUEUE_NAME,
                                  autoAck: true,
                                  consumer: consumer);

            await Task.WhenAny(tcs.Task, Task.Delay(100000));


            return messages;
        }
    }
}

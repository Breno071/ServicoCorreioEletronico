using AppRepository.Data;
using AppRepository.Entities;
using AppRepository.Interfaces;
using AppRepository.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace ConsumerWindowsService
{
    public class Worker : BackgroundService
    {
        private readonly ConnectionFactory _factory = new()
        {
            HostName = "localhost"
        };
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IModel _channel;
        private const string QUEUE_NAME = "Adoptions";
        private ApplicationContext _context;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    }

                    await ConsumeRabbitMessages();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                } 
            }
        }

        internal async Task ConsumeRabbitMessages()
        {
            await Task.Delay(1000);
            IAdoptionRepository adoptionRepository;

            _channel.QueueDeclare(queue: QUEUE_NAME,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
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
                        {
                            request.Id = Guid.NewGuid();
                            request.Adopter.Id = Guid.NewGuid();

                            using (var scope = _serviceProvider.CreateScope())
                            {
                                _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                                adoptionRepository = new AdoptionRepository(_context);
                                adoptionRepository.Insert(request);
                            }
                        }
                        else
                            throw new Exception($"Não foi possível deserializar a mensagem {message}");
                    }
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
                catch (Exception e)
                {
                    Console.WriteLine("Erro fatal: " + e.Message);
                }
            };

            _channel.BasicConsume(queue: QUEUE_NAME,
                                  autoAck: true,
                                  consumer: consumer);
        }
    }
}

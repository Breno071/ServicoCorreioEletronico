using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace ConsumerWindowsService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Consumer _consumer;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _consumer = new();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                try
                {
                    List<string> messages =  await _consumer.GetMessages();

                    if (messages.Count > 0)
                    {
                        //Insert na base para tabela de envio de emails
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}

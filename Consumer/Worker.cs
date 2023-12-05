using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ClientConsumer _consumer;

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
                    _consumer.Consume();
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

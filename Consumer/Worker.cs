using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumer _consumer;

        public Worker(ILogger<Worker> logger, IConsumer consumer)
        {
            _logger = logger;
            _consumer = consumer;
        }

        public async Task StartConsumer()
        {
           await _consumer.Consume();
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
                    await StartConsumer();
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

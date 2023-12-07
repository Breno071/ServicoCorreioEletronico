using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using AppRepository.Data;

namespace ConsumerWindowsService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly Consumer _consumer;
        private ApplicationContext _context;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _consumer = new();
            _serviceProvider = serviceProvider;
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
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                        await Rotina();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                await Task.Delay(10000, stoppingToken);
            }
        }

        private async Task Rotina()
        {
            List<string> messages = await _consumer.GetMessages();

            if (messages.Count > 0)
            {
                //Insert na base para tabela de envio de emails
            }
        }
    }
}

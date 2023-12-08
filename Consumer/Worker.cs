using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using AppRepository.Data;
using AppRepository.Interfaces;
using AppRepository.Repository;
using Utills.Models;
using AppRepository.Entities;

namespace ConsumerWindowsService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private Consumer _consumer;
        private ApplicationContext _context;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
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
                        _consumer = new Consumer(_context);
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
            List<AdoptRequest> adoptions = await _consumer.GetMessages();
            IEmailRepository emailRepository = new EmailRepository(_context);

            if (adoptions.Count > 0)
            {
                List<PendentEmail> pendentEmails = new List<PendentEmail>();
                foreach (var adoption in adoptions)
                {
                    pendentEmails.Add(new PendentEmail
                    {
                        Email = new Email
                        {
                            From = "bns734683@gmail.com",
                            To = adoption.Adopter.Email,
                            Subject = adoption.Adopter.Nome,
                            Body = "Body"
                        },
                    });
                }
                await emailRepository.BulkInsert(pendentEmails);
            }
        }
    }
}

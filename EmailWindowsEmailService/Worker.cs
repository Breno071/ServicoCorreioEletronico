using AppRepository.Data;
using AppRepository.Entities;
using AppRepository.Interfaces;
using AppRepository.Repository;
using Utills;
using Utills.Interfaces;
using Utills.Security;
using Utills.Services;

namespace EmailWindowsEmailService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
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
                    _logger.LogInformation("Consume Scoped Service Hosted Service is working.");
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        _context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                        await Rotina();
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task Rotina()
        {
            IEmailRepository emailRepository = new EmailRepository(_context ?? throw new Exception("ApplicationContext is null, cannot continue"));
            ILogRepository logRepository = new LogRepository(_context ?? throw new Exception("ApplicationContext is null, cannot continue"));
            IEmailService emailService = new EmailService();

            //Busca na base os emails a serem enviados e enviar
            List<PendentEmail> listaEmailsNaoProcessados = await emailRepository.GetNotProcessedEmails();

            foreach (var email in listaEmailsNaoProcessados)
            {
                try
                {
                    email.Email.FromPassword = CriptografyService.Descriptografar(email.Email.FromPassword);
                    bool sucesso = emailService.SendEmail(email.Email);

                    if (sucesso)
                    {
                        email.Processed = true;
                        email.Email.FromPassword = CriptografyService.Criptografar(email.Email.FromPassword);
                        //Marca como processados os emails que foram enviados
                        await emailRepository.Update(email);
                    }
                }
                catch (Exception e)
                {
                    logRepository.Add(new Log()
                    {
                        LogType = LogType.Error,
                        Message = e.Message
                    });
                }

                await Task.Delay(1000);
            }

        }
    }
}

using AppRepository.Interfaces;
using AppRepository.Repository;
using Utills;
using Utills.Interfaces;
using Utills.Models;

namespace EmailWindowsEmailService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
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
                    await Rotina();
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
            IEmailRepository emailRepository = new EmailRepository();
            IEmailService emailService = new EmailService();

            //TODO Buscar na base os emails a serem enviados e enviar
            List<Email> listaEmailsNaoProcessados = await emailRepository.GetNotProcessedEmails();

            foreach (var email in listaEmailsNaoProcessados)
            {
                bool sucesso = emailService.SendEmail(email);

                if (sucesso)
                    email.Processed = true;
                else
                    email.Processed = false;

                //TODO Marcar como processados os emails que foram enviados
                await Task.Delay(1000);
            }

            //TODO Gravar log de envio(Quais deram sucesso e quais deram falha)
        }
    }
}

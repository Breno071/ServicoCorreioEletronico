using API.Interfaces;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensagemController : ControllerBase
    {
        private readonly IProducer _producer;
        public MensagemController(IProducer producer)
        {
            _producer = producer;
        }

        [HttpPost("post-message")]
        public async Task<IActionResult> PostMessage([FromBody] MensagemInputModel mensagem)
        {
            EmailService emailService = new();
            emailService.SendEmail();
            List<Task> tarefas = new()
            {
                Task.Run(() =>
                {
                     _producer.Send(mensagem);
                })
            };

            await Task.WhenAll(tarefas);

            return Accepted();
        }
    }
}

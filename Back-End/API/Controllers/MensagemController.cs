using API.Models;
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
        private readonly ConnectionFactory _factory;
        private const string QUEUE_NAME = "Mensagens";
        public MensagemController(ConnectionFactory factory)
        {
            _factory = factory;
        }

        [HttpPost("post-message")]
        public async Task<IActionResult> PostMessage([FromBody] MensagemInputModel mensagem)
        {
            List<Task> tarefas = new()
            {
                Task.Run(() =>
                {
                     using (var connection = _factory.CreateConnection())
                    {
                        using (var channel = connection.CreateModel())
                        {
                            channel.QueueDeclare(
                                QUEUE_NAME,
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null
                                );

                            var stringfiedMessage = JsonSerializer.Serialize(mensagem);
                            var bytesMessage = System.Text.Encoding.UTF8.GetBytes(stringfiedMessage);

                            channel.BasicPublish(
                                exchange: "",
                                routingKey: QUEUE_NAME,
                                basicProperties: null,
                                body: bytesMessage
                                );
                        }
                    }
                })
            };

            await Task.WhenAll(tarefas);

            return Accepted();
        }
    }
}

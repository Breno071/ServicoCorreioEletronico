using AppRepository.Entities;
using Microsoft.AspNetCore.Mvc;
using Producer;

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
        public async Task<IActionResult> PostMessage([FromBody] AdoptRequest message)
        {
            List<Task> tarefas = new()
            {
                Task.Run(() =>
                {
                     _producer.Send(message);
                })
            };

            await Task.WhenAll(tarefas);

            return Accepted();
        }
    }
}

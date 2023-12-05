
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Producer;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptController : ControllerBase
    {
        private readonly IProducer _producer;

        public AdoptController(IProducer producer)
        {
            _producer = producer;
        }

        [HttpPost("adopt")]
        public async Task<IActionResult> Adopt([FromBody] AdoptRequest adoption)
        {
            List<Task> tarefas = new()
            {
                Task.Run(() =>
                {
                     _producer.Send(adoption);
                })
            };

            await Task.WhenAll(tarefas);
            return Accepted();
        }
    }
}

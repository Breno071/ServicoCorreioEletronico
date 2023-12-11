using Microsoft.AspNetCore.Mvc;
using Producer;
using Utills.Models;
using Utills.Validations;

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
            if (!Validations.IsEmailValid(adoption.Adopter.Email))
                return BadRequest("E-mail inválido!");

            if(!Validations.IsCpfValid(adoption.Adopter.CPF))
                return BadRequest("CPF inválido!");

            List<Task> tarefas = new()
            {
                Task.Run(() =>
                {
                     _producer.Send(adoption);
                })
            };

            await Task.WhenAll(tarefas);
            return Accepted(adoption);
        }
    }
}

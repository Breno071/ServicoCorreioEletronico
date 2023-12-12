using API.DTOs;
using AppRepository.Entities;
using Microsoft.AspNetCore.Mvc;
using Producer;
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
        public async Task<IActionResult> Adopt([FromBody] AdopterRequestDTO adoption)
        {
            if (!Validations.IsEmailValid(adoption.Adopter.Email))
                return BadRequest("E-mail inválido!");

            if(!Validations.IsCpfValid(adoption.Adopter.CPF))
                return BadRequest("CPF inválido!");

            AdoptRequest request = new()
            {
                Adopter = new Adopter()
                {
                    CPF = adoption.Adopter.CPF,
                    Email = adoption.Adopter.Email,
                    Nome = adoption.Adopter.Nome,
                    DataNascimento = adoption.Adopter.DataNascimento
                },
                Breed = adoption.Breed,
                AnimalType = adoption.AnimalType
            };

            await Task.Run(() =>
            {
                _producer.Send(request);
            });

            return Accepted(request);
        }
    }
}

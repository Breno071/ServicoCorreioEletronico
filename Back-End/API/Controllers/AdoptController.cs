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
        public async Task<IActionResult> Adopt([FromBody] AdopterRequestDTO adoptionRequest)
        {
            if (!Validations.IsEmailValid(adoptionRequest.Adopter.Email))
                return BadRequest("E-mail inválido!");

            if(!Validations.IsCpfValid(adoptionRequest.Adopter.CPF))
                return BadRequest("CPF inválido!");

            AdoptRequest adoption = new()
            {
                Adopter = new Adopter()
                {
                    CPF = adoptionRequest.Adopter.CPF,
                    Email = adoptionRequest.Adopter.Email,
                    Nome = adoptionRequest.Adopter.Nome,
                    DataNascimento = adoptionRequest.Adopter.DataNascimento
                },
                Specie = adoptionRequest.Specie,
                Age = adoptionRequest.Age,
                Sex = adoptionRequest.Sex,
                AnimalType = adoptionRequest.AnimalType
            };

            await Task.Run(() =>
            {
                _producer.Send(adoption);
            });

            return Accepted(adoptionRequest);
        }

        [HttpGet("list-pets")]
        public async Task<IActionResult> ListPets(string specie, string age, char sex)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api-staging.adoptapet.com/search/pet_search?key=hg4nsv85lppeoqqixy3tnlt3k8lj6o0c&v=3&output=json&city_or_zip=47374&geo_range=50&species={specie.ToLower()}&breed_id=real=801&sex={sex}&age={age}&color_id=54&pet_size_range_id=2&hair=&bonded_pair=&special_needs=&include_mixes=&added_after=&start_number=1&end_number=50&meta_only=0");
            request.Headers.Add("Accept", "application/json");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return Ok(await response.Content.ReadFromJsonAsync<object>());
        }

        [HttpGet("datails/{pet_id:long}")]
        public async Task<IActionResult> GetPetDetails(long pet_id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api-staging.adoptapet.com/search/pet_details?pet_id={pet_id}&key=hg4nsv85lppeoqqixy3tnlt3k8lj6o0c&v=3&output=json");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return Ok(await response.Content.ReadFromJsonAsync<object>());
        }
    }
}

﻿using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("adopt")]
        public async Task<IActionResult> Adopt([FromBody] AdoptRequest message)
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

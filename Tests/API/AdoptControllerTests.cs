using API.Controllers;
using API.DTOs;
using AppRepository.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Producer;
using Utills.Interfaces;

namespace Tests.API
{
    public class AdoptControllerTests
    {
        [Fact]
        public async Task DeveRetornarBadRequestParaEmailInvalido()
        {
            // Arrange
            var producer = new Mock<IProducer>();
            var controller = new AdoptController(producer.Object);
            var adoptionRequest = new AdopterRequestDTO
            {
                Adopter = new AdopterDTO { Email = "ASD" }
            };

            // Act
            var result = await controller.Adopt(adoptionRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal("E-mail ou CPF inválidos!", badRequestResult?.Value);
        }

        [Fact]
        public async Task DeveRetornarBadRequestParaCpfInvalido()
        {
            // Arrange
            var producer = new Mock<IProducer>();
            var controller = new AdoptController(producer.Object);
            var adoptionRequest = new AdopterRequestDTO
            {
                Adopter = new AdopterDTO { CPF = "123" }
            };

            // Act
            var result = await controller.Adopt(adoptionRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal("E-mail ou CPF inválidos!", badRequestResult?.Value);
        }

        [Fact]
        public async Task DeveRetornarAcceptedParaAdocaoBemSucedida()
        {
            // Arrange
            var mockProducer = new Mock<IProducer>();
            var controller = new AdoptController(mockProducer.Object);
            var adoptionRequest = new AdopterRequestDTO
            {
                Adopter = new AdopterDTO
                {
                    Nome = "Breno",
                    Email = "brenonarde@gmail.com",
                    CPF = "07211814578",
                    DataNascimento = DateTime.ParseExact("14/11/1997", "dd/MM/yyyy", null)
                },
            };

            // Act
            var result = await controller.Adopt(adoptionRequest);

            // Assert
            Assert.IsType<AcceptedResult>(result);
            var acceptedResult = result as AcceptedResult;
            Assert.Equal(adoptionRequest, acceptedResult?.Value);

            // Verificar se o método Send foi chamado no produtor
            mockProducer.Verify(p => p.Send(It.IsAny<AdoptRequest>()), Times.Once);
        }
    }
}

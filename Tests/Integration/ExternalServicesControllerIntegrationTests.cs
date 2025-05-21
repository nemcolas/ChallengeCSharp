using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OdontoPrevCSharp.Controllers;
using OdontoPrevCSharp.Models.Dtos;
using OdontoPrevCSharp.Services;
using System.Threading.Tasks;

namespace OdontoPrevCSharp.Tests.Integration
{
    [TestClass]
    public class ExternalServicesControllerIntegrationTests
    {
        private ExternalServicesController _controller;
        private Mock<IViaCepService> _mockViaCepService;

        [TestInitialize]
        public void Setup()
        {
            _mockViaCepService = new Mock<IViaCepService>();
            _controller = new ExternalServicesController(_mockViaCepService.Object);
        }

        [TestMethod]
        public async Task GetAddressByCep_ValidCep_ReturnsOkResult()
        {
            // Arrange
            var cep = "01001000";
            var expectedResponse = new ViaCepResponse
            {
                Cep = "01001-000",
                Logradouro = "Praça da Sé",
                Complemento = "lado ímpar",
                Bairro = "Sé",
                Localidade = "São Paulo",
                Uf = "SP",
                Ibge = "3550308",
                Gia = "1004",
                Ddd = "11",
                Siafi = "7107"
            };

            _mockViaCepService.Setup(s => s.GetAddressByCepAsync(cep))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetAddressByCep(cep);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            
            var returnValue = okResult.Value as ViaCepResponse;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(expectedResponse.Cep, returnValue.Cep);
            Assert.AreEqual(expectedResponse.Logradouro, returnValue.Logradouro);
            Assert.AreEqual(expectedResponse.Bairro, returnValue.Bairro);
        }

        [TestMethod]
        public async Task GetAddressByCep_InvalidCep_ReturnsBadRequest()
        {
            // Arrange
            var cep = "123"; // CEP inválido (menos de 8 dígitos)

            // Act
            var result = await _controller.GetAddressByCep(cep);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAddressByCep_CepNotFound_ReturnsNotFound()
        {
            // Arrange
            var cep = "00000000"; // CEP não encontrado
            _mockViaCepService.Setup(s => s.GetAddressByCepAsync(cep))
                .ReturnsAsync((ViaCepResponse)null);

            // Act
            var result = await _controller.GetAddressByCep(cep);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OdontoPrevCSharp.Models.Dtos;
using OdontoPrevCSharp.Services;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Moq.Protected;

namespace OdontoPrevCSharp.Tests.Unit
{
    [TestClass]
    public class ViaCepServiceTests
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _httpClient;
        private ViaCepService _viaCepService;

        [TestInitialize]
        public void Setup()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new System.Uri("https://viacep.com.br/")
            };
            _viaCepService = new ViaCepService(_httpClient, new Logger<ViaCepService>(new LoggerFactory()));
        }

        [TestMethod]
        public async Task GetAddressByCepAsync_ValidCep_ReturnsAddress()
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

            var jsonResponse = JsonSerializer.Serialize(expectedResponse);
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            // Act
            var result = await _viaCepService.GetAddressByCepAsync(cep);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse.Cep, result.Cep);
            Assert.AreEqual(expectedResponse.Logradouro, result.Logradouro);
            Assert.AreEqual(expectedResponse.Bairro, result.Bairro);
            Assert.AreEqual(expectedResponse.Localidade, result.Localidade);
            Assert.AreEqual(expectedResponse.Uf, result.Uf);
        }

        [TestMethod]
        public async Task GetAddressByCepAsync_InvalidCep_ReturnsNull()
        {
            // Arrange
            var cep = "00000000";
            var jsonResponse = "{\"erro\": true}";
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            // Act
            var result = await _viaCepService.GetAddressByCepAsync(cep);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAddressByCepAsync_HttpError_ReturnsNull()
        {
            // Arrange
            var cep = "01001000";
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            // Act
            var result = await _viaCepService.GetAddressByCepAsync(cep);

            // Assert
            Assert.IsNull(result);
        }
    }
}

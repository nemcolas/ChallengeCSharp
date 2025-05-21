using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OdontoPrevCSharp.Controllers;
using OdontoPrevCSharp.Models.ML;
using OdontoPrevCSharp.Services;
using System.Threading.Tasks;

namespace OdontoPrevCSharp.Tests.Integration
{
    [TestClass]
    public class PredictionControllerIntegrationTests
    {
        private PredictionController _controller;
        private Mock<PredictionService> _mockPredictionService;

        [TestInitialize]
        public void Setup()
        {
            _mockPredictionService = new Mock<PredictionService>();
            _controller = new PredictionController(_mockPredictionService.Object, Mock.Of<ILogger<PredictionController>>());
        }

        [TestMethod]
        public void Predict_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var input = new SinistroInput
            {
                Idade = 45,
                NumeroSinistrosAnteriores = 2,
                TempoDesdeUltimoSinistro = 12,
                CustoMedioSinistroAnterior = 500.0f
            };

            var expectedPrediction = new SinistroPrediction
            {
                Probability = 0.75f,
                Prediction = true
            };

            _mockPredictionService.Setup(s => s.Predict(It.IsAny<SinistroInput>()))
                .Returns(expectedPrediction);

            // Act
            var result = _controller.PredictSinistroRisk(input);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            
            var returnValue = okResult.Value as SinistroPrediction;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(expectedPrediction.Probability, returnValue.Probability);
            Assert.AreEqual(expectedPrediction.Prediction, returnValue.Prediction);
        }

        [TestMethod]
        public void Predict_NullInput_ReturnsBadRequest()
        {
            // Act
            var result = _controller.PredictSinistroRisk(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [TestMethod]
        public void Predict_InvalidInput_ReturnsDefaultPrediction()
        {
            // Arrange
            var input = new SinistroInput
            {
                // Dados inválidos ou incompletos
                Idade = -1,
                NumeroSinistrosAnteriores = -1,
                TempoDesdeUltimoSinistro = -1,
                CustoMedioSinistroAnterior = -1.0f
            };

            var defaultPrediction = new SinistroPrediction
            {
                Probability = 0.0f,
                Prediction = false
            };

            _mockPredictionService.Setup(s => s.Predict(It.IsAny<SinistroInput>()))
                .Returns(defaultPrediction);

            // Act
            var result = _controller.PredictSinistroRisk(input);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            
            var returnValue = okResult.Value as SinistroPrediction;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(defaultPrediction.Probability, returnValue.Probability);
            Assert.AreEqual(defaultPrediction.Prediction, returnValue.Prediction);
        }
    }
}

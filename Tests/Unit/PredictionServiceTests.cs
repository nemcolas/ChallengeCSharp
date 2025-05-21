using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OdontoPrevCSharp.Models.ML;
using OdontoPrevCSharp.Services;
using System.Threading.Tasks;

namespace OdontoPrevCSharp.Tests.Unit
{
    [TestClass]
    public class PredictionServiceTests
    {
        private PredictionService _predictionService;
        private Mock<ModelTrainerService> _mockModelTrainerService;

        [TestInitialize]
        public void Setup()
        {
            _mockModelTrainerService = new Mock<ModelTrainerService>();
            _predictionService = new PredictionService();
        }

        [TestMethod]
        public void Predict_ValidInput_ReturnsPrediction()
        {
            // Arrange
            var input = new SinistroInput
            {
                Idade = 45,
                NumeroSinistrosAnteriores = 2,
                TempoDesdeUltimoSinistro = 12,
                CustoMedioSinistroAnterior = 500.0f
            };

            // Act
            var result = _predictionService.Predict(input);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SinistroPrediction));
            // Não podemos testar o valor exato da predição, pois depende do modelo treinado
            // Mas podemos verificar se está dentro de um intervalo válido (0-1 para probabilidade)
            Assert.IsTrue(result.Probability >= 0 && result.Probability <= 1);
        }

        [TestMethod]
        public void Predict_NullInput_ReturnsDefaultPrediction()
        {
            // Act
            var result = _predictionService.Predict(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SinistroPrediction));
            Assert.AreEqual(0, result.Probability);
            Assert.IsFalse(result.Prediction);
        }

        [TestMethod]
        public void Predict_ExtremeValues_HandlesGracefully()
        {
            // Arrange
            var input = new SinistroInput
            {
                Idade = 120, // Idade improvável
                NumeroSinistrosAnteriores = 50, // Valor extremo
                TempoDesdeUltimoSinistro = 0,
                CustoMedioSinistroAnterior = 99999.0f // Valor extremo
            };

            // Act
            var result = _predictionService.Predict(input);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SinistroPrediction));
            // Apenas verificamos se não lança exceção e retorna um resultado válido
            Assert.IsTrue(result.Probability >= 0 && result.Probability <= 1);
        }
    }
}

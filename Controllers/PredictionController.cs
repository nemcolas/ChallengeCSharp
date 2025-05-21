using Microsoft.AspNetCore.Mvc;
using OdontoPrevCSharp.Models.ML;
using OdontoPrevCSharp.Services;

namespace OdontoPrevCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly IPredictionService _predictionService;
        private readonly ILogger<PredictionController> _logger; // Optional: Add logging

        public PredictionController(IPredictionService predictionService, ILogger<PredictionController> logger)
        {
            _predictionService = predictionService;
            _logger = logger;
        }


        /// Prevê o risco de sinistro com base nos dados fornecidos
        /// Dados de entrada para a predição (Idade, NumeroSinistrosAnteriores, TempoDesdeUltimoSinistro, CustoMedioSinistroAnterior).
        /// resultado da predição (Risco Alto/Baixo, Score, Probabilidade).
        [HttpPost("sinistro-risk")]
        [ProducesResponseType(typeof(SinistroPrediction), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PredictSinistroRisk([FromBody] SinistroInput input)
        {
            // Basic validation (can be more robust using FluentValidation or DataAnnotations)
            if (input == null)
            {
                return BadRequest("Input data is required.");
            }
            
            try
            {
                var prediction = _predictionService.Predict(input);
                _logger.LogInformation("Prediction executed successfully.");
                return Ok(prediction);
            }
            catch (FileNotFoundException ex) 
            {
                 _logger.LogError(ex, "Prediction failed: Model file not found.");
                 return StatusCode(StatusCodes.Status500InternalServerError, "Prediction model is not available. Please ensure it has been trained.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during prediction.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while processing the prediction.");
            }
        }
    }
}


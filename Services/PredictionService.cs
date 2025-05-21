using Microsoft.ML;
using OdontoPrevCSharp.Models.ML;

namespace OdontoPrevCSharp.Services
{
    // passei exatas 48 horas nessa bomba tenha piedade fessor
    public interface IPredictionService
    {
        SinistroPrediction Predict(SinistroInput input);
    }

    public class PredictionService : IPredictionService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _trainedModel;
        private readonly string _modelPath = Path.Combine(AppContext.BaseDirectory, "MLModels", "sinistro_model.zip");

        public PredictionService()
        {
            _mlContext = new MLContext(seed: 0);

            // Load the trained model
            if (!File.Exists(_modelPath))
            {

                Console.WriteLine($"Model file not found at: {_modelPath}. Ensure the model is trained and saved first.");
                throw new FileNotFoundException("Trained model not found.", _modelPath);
            }

            _trainedModel = _mlContext.Model.Load(_modelPath, out var modelSchema);
        }

        public SinistroPrediction Predict(SinistroInput input)
        {
            // Crio o prediction engine 
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<SinistroInput, SinistroPrediction>(_trainedModel);

            // Faz a previsão
            var prediction = predictionEngine.Predict(input);

            return prediction;
        }
    }
}


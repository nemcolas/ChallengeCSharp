using Microsoft.ML;
using OdontoPrevCSharp.Models.ML;

namespace OdontoPrevCSharp.Services
{
    public class ModelTrainerService
    {
        private readonly MLContext _mlContext;
        private readonly string _dataPath = Path.Combine(AppContext.BaseDirectory, "Data", "ML", "sinistro_data.csv");
        private readonly string _modelPath = Path.Combine(AppContext.BaseDirectory, "MLModels", "sinistro_model.zip");

        public ModelTrainerService()
        {
            _mlContext = new MLContext(seed: 0);
        }

        public void TrainAndSaveModel()
        {

            var modelDir = Path.GetDirectoryName(_modelPath);
            if (!Directory.Exists(modelDir))
            {
                Directory.CreateDirectory(modelDir);
            }
            
            if (!File.Exists(_dataPath))
            {
                Console.WriteLine($"Data file not found at: {_dataPath}");

                CreateDummyDataFileIfNotExists();

            }

            // 1. Load Data
            IDataView dataView = _mlContext.Data.LoadFromTextFile<SinistroInput>(_dataPath, separatorChar: ',', hasHeader: true);



            // Define o pipeline de transformação e treinamento
            var pipeline = _mlContext.Transforms.Concatenate("Features", nameof(SinistroInput.Idade),
                                                                    nameof(SinistroInput.NumeroSinistrosAnteriores),
                                                                    nameof(SinistroInput.TempoDesdeUltimoSinistro),
                                                                    nameof(SinistroInput.CustoMedioSinistroAnterior))
                // Use a simple trainer for demonstration. Choose the best one based on data and problem.
                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: nameof(SinistroInput.RiscoSinistro), featureColumnName: "Features"));

            // 3. Train Model
            Console.WriteLine("=============== Training the model ===============");

            ITransformer trainedModel = pipeline.Fit(dataView); 
            Console.WriteLine("=============== Finished training ===============");
            

            // 4. salvano o modelo
            Console.WriteLine($"=============== Saving the model to {_modelPath} ===============");
            _mlContext.Model.Save(trainedModel, dataView.Schema, _modelPath);
            Console.WriteLine("=============== Model saved ===============");
        }
        
        private void CreateDummyDataFileIfNotExists() // cria um arquivo de dados fictício 
        {
            var dataDir = Path.GetDirectoryName(_dataPath);
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }
            if (!File.Exists(_dataPath))
            {
                Console.WriteLine($"Creating dummy data file at: {_dataPath}");
                File.WriteAllText(_dataPath, 
                    "Idade,NumeroSinistrosAnteriores,TempoDesdeUltimoSinistro,CustoMedioSinistroAnterior,RiscoSinistro\n" +
                    "25,0,12,0,0\n" +
                    "45,2,3,150.50,1\n" +
                    "60,1,24,80.00,0\n" +
                    "30,3,1,250.75,1\n" +
                    "55,0,36,0,0\n");
            }
        }
    }
}


using Microsoft.ML.Data;

namespace OdontoPrevCSharp.Models.ML
{
    // saida do modelo
    public class SinistroPrediction
    {

        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

 
        public float Score { get; set; }


        public float Probability { get; set; }
    }
}


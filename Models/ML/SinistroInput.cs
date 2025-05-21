using Microsoft.ML.Data;

namespace OdontoPrevCSharp.Models.ML
{
    // estrutura de dados pra treinamento do modelo 
    public class SinistroInput
    {
        
        [LoadColumn(0)]
        public float Idade { get; set; }

        [LoadColumn(1)]
        public float NumeroSinistrosAnteriores { get; set; }

        [LoadColumn(2)]
        public float TempoDesdeUltimoSinistro { get; set; } // e.g., in months

        [LoadColumn(3)]
        public float CustoMedioSinistroAnterior { get; set; }



        // Atributo de saída (o que queremos prever)
        [LoadColumn(4)] 
        public bool RiscoSinistro { get; set; } // True = alto risco , False = baixo risco
    }
}


using System;
using System.ComponentModel.DataAnnotations;

namespace OdontoPrevCSharp.Models
{
    public class Tratamento
    {
        [Key]
        public int IdTratamento { get; set; }
        public string TipoTratamento { get; set; }
        public string Descricao { get; set; }
        public float Custo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int ConsultaId { get; set; }
        public Consulta Consulta { get; set; }
    }
}
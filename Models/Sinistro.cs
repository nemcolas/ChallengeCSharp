using System;
using System.ComponentModel.DataAnnotations;

namespace OdontoPrevCSharp.Models
{
    public class Sinistro
    {
        [Key]
        public int IdSinistro { get; set; }
        public string MotivoSinistro { get; set; }
        public DateTime DataAbertura { get; set; }
        public string StatusSinistro { get; set; }
        public int ConsultaId { get; set; }
        public Consulta Consulta { get; set; }
    }
}
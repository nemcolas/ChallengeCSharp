using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoPrevCSharp.Models
{
    [Table("consulta")] 
    public class Consulta
    {
        [Key]
        public int IdConsulta { get; set; }
        public DateTime DataConsulta { get; set; }
        public string TipoConsulta { get; set; }
        public float Custo { get; set; }
        public string StatusSinistro { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public int DentistaId { get; set; }
        public Dentista Dentista { get; set; }
    }
}
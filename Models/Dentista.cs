using System.ComponentModel.DataAnnotations;

namespace OdontoPrevCSharp.Models
{
    public class Dentista
    {
        [Key]
        public int IdDentista { get; set; }
        public string Nome { get; set; }
        public string Cro { get; set; }
        public string Especialidade { get; set; }
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
    }
}
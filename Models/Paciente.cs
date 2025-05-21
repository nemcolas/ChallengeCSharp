using System;
using System.ComponentModel.DataAnnotations;

namespace OdontoPrevCSharp.Models
{
    public class Paciente
    {
        [Key]
        public int IdPaciente { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
    }
}
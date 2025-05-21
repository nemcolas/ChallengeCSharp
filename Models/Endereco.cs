using System.ComponentModel.DataAnnotations;

namespace OdontoPrevCSharp.Models
{
    public class Endereco
    {
        [Key]
        public int IdEndereco { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }
}
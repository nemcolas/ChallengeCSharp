using System.ComponentModel.DataAnnotations;

namespace OdontoPrevCSharp.Models
{
    public class Genero
    {
        [Key]
        public int IdGenero { get; set; }
        public string Descricao { get; set; }
    }
}
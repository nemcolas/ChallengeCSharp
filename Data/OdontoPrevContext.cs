using Microsoft.EntityFrameworkCore;
using OdontoPrevCSharp.Models;

namespace OdontoPrevCSharp.Data
{
    public class OdontoPrevContext : DbContext
    {
        public OdontoPrevContext(DbContextOptions<OdontoPrevContext> options)
            : base(options)
        {
        }

        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Dentista> Dentistas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Sinistro> Sinistros { get; set; }
        public DbSet<Tratamento> Tratamentos { get; set; }
    }
}
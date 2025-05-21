using OdontoPrevCSharp.Models;

namespace OdontoPrevCSharp.Repositories
{
    public interface IDentistaRepository : IRepository<Dentista>
    {
        // Obter dentista por CRO
        Task<Dentista> GetDentistaByCroAsync(string cro);

        // Obter dentistas por especialidade
        Task<IEnumerable<Dentista>> GetDentistasByEspecialidadeAsync(string especialidade);
    }
}

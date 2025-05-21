using OdontoPrevCSharp.Models;

namespace OdontoPrevCSharp.Repositories
{
    public interface ISinistroRepository : IRepository<Sinistro>
    {
        // Obter sinistros por consulta
        Task<IEnumerable<Sinistro>> GetSinistrosByConsultaIdAsync(int consultaId);

        // Obter sinistros por status (aberto, fechado, etc.)
        Task<IEnumerable<Sinistro>> GetSinistrosByStatusAsync(string status);
    }
}



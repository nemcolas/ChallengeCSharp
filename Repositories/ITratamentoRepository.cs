using OdontoPrevCSharp.Models;

namespace OdontoPrevCSharp.Repositories
{
    public interface ITratamentoRepository : IRepository<Tratamento>
    {
        // Obter tratamentos por tipo
        Task<IEnumerable<Tratamento>> GetTratamentosByTipoAsync(string tipo);

        // Obter tratamentos por data de início
        Task<IEnumerable<Tratamento>> GetTratamentosByDataInicioAsync(DateTime dataInicio);
    }
}
using OdontoPrevCSharp.Models;

namespace OdontoPrevCSharp.Repositories
{
    public interface IConsultaRepository : IRepository<Consulta>
    {
        // Obter consultas de um paciente específico
        Task<IEnumerable<Consulta>> GetConsultasByPacienteIdAsync(int pacienteId);

        // Obter consultas por um intervalo de datas
        Task<IEnumerable<Consulta>> GetConsultasByDateRangeAsync(DateTime startDate, DateTime endDate);

        // Obter consultas pendentes (sinistro em aberto)
        Task<IEnumerable<Consulta>> GetConsultasPendentesAsync();
    }
}
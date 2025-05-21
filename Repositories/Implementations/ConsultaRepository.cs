using Microsoft.EntityFrameworkCore;
using OdontoPrevCSharp.Data;
using OdontoPrevCSharp.Models;
using OdontoPrevCSharp.Repositories;

namespace OdontoPrevCSharp.Repositories.Implementations
{
    public class ConsultaRepository : Repository<Consulta>, IConsultaRepository
    {
        private readonly OdontoPrevContext _context;

        public ConsultaRepository(OdontoPrevContext context) : base(context)
        {
            _context = context;
        }

        // Obter consultas de um paciente específico
        public async Task<IEnumerable<Consulta>> GetConsultasByPacienteIdAsync(int pacienteId)
        {
            return await _context.Consultas
                .Where(c => c.PacienteId == pacienteId)
                .ToListAsync();
        }

        // Obter consultas por um intervalo de datas
        public async Task<IEnumerable<Consulta>> GetConsultasByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Consultas
                .Where(c => c.DataConsulta >= startDate && c.DataConsulta <= endDate)
                .ToListAsync();
        }

        // Obter consultas pendentes (sinistro em aberto)
        public async Task<IEnumerable<Consulta>> GetConsultasPendentesAsync()
        {
            return await _context.Consultas
                .Where(c => c.StatusSinistro == "Aberto")
                .ToListAsync();
        }
    }
}
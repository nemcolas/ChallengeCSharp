using Microsoft.EntityFrameworkCore;
using OdontoPrevCSharp.Data;
using OdontoPrevCSharp.Models;
using OdontoPrevCSharp.Repositories;

namespace OdontoPrevCSharp.Repositories.Implementations
{
    public class SinistroRepository : Repository<Sinistro>, ISinistroRepository
    {
        private readonly OdontoPrevContext _context;

        public SinistroRepository(OdontoPrevContext context) : base(context)
        {
            _context = context;
        }

        // Obter sinistros por consulta
        public async Task<IEnumerable<Sinistro>> GetSinistrosByConsultaIdAsync(int consultaId)
        {
            return await _context.Sinistros
                .Where(s => s.ConsultaId == consultaId)
                .ToListAsync();
        }

        // Obter sinistros por status (aberto, fechado, etc.)
        public async Task<IEnumerable<Sinistro>> GetSinistrosByStatusAsync(string status)
        {
            return await _context.Sinistros
                .Where(s => s.StatusSinistro == status)
                .ToListAsync();
        }
    }
}
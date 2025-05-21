using Microsoft.EntityFrameworkCore;
using OdontoPrevCSharp.Data;
using OdontoPrevCSharp.Models;
using OdontoPrevCSharp.Repositories;

namespace OdontoPrevCSharp.Repositories.Implementations
{
    public class DentistaRepository : Repository<Dentista>, IDentistaRepository
    {
        private readonly OdontoPrevContext _context;

        public DentistaRepository(OdontoPrevContext context) : base(context)
        {
            _context = context;
        }

        // Obter dentista por CRO
        public async Task<Dentista> GetDentistaByCroAsync(string cro)
        {
            return await _context.Dentistas
                .FirstOrDefaultAsync(d => d.Cro == cro);
        }

        // Obter dentistas por especialidade
        public async Task<IEnumerable<Dentista>> GetDentistasByEspecialidadeAsync(string especialidade)
        {
            return await _context.Dentistas
                .Where(d => d.Especialidade == especialidade)
                .ToListAsync();
        }
    }
}
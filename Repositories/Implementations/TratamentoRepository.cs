using Microsoft.EntityFrameworkCore;
using OdontoPrevCSharp.Data;
using OdontoPrevCSharp.Models;
using OdontoPrevCSharp.Repositories;

namespace OdontoPrevCSharp.Repositories.Implementations
{
    public class TratamentoRepository : Repository<Tratamento>, ITratamentoRepository
    {
        private readonly OdontoPrevContext _context;

        public TratamentoRepository(OdontoPrevContext context) : base(context)
        {
            _context = context;
        }

        // Obter tratamentos por tipo
        public async Task<IEnumerable<Tratamento>> GetTratamentosByTipoAsync(string tipo)
        {
            return await _context.Tratamentos
                .Where(t => t.TipoTratamento == tipo)
                .ToListAsync();
        }

        // Obter tratamentos por data de início
        public async Task<IEnumerable<Tratamento>> GetTratamentosByDataInicioAsync(DateTime dataInicio)
        {
            return await _context.Tratamentos
                .Where(t => t.DataInicio >= dataInicio)
                .ToListAsync();
        }
    }
}
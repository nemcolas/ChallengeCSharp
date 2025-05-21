using Microsoft.EntityFrameworkCore;
using OdontoPrevCSharp.Data;
using OdontoPrevCSharp.Models;
using OdontoPrevCSharp.Repositories;

namespace OdontoPrevCSharp.Repositories.Implementations
{
    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        private readonly OdontoPrevContext _context;

        public PacienteRepository(OdontoPrevContext context) : base(context)
        {
            _context = context;
        }

        // Obter paciente por CPF
        public async Task<Paciente> GetPacienteByCpfAsync(string cpf)
        {
            return await _context.Pacientes
                .FirstOrDefaultAsync(p => p.Cpf == cpf);
        }

        // Obter todos os pacientes por gênero
        public async Task<IEnumerable<Paciente>> GetPacientesByGeneroAsync(int generoId)
        {
            return await _context.Pacientes
                .Where(p => p.GeneroId == generoId)
                .ToListAsync();
        }
    }
}